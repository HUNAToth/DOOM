using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG{
    public class EnemyManager  : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyStats enemyStats;
        public NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidbody;

        public State currentState;
        
        public CharacterStats currentTarget;  

        public bool isPerformingAction;
        public bool isInteracting;


       // public EnemyAttackAction[] enemyAttacks;
       // public EnemyAttackAction currentAttack;


        [Header("AI Settings")]
        public float detectionRadius = 20.0f;
        public float minimumDetectionAngle = -50;
        public float maximumDetectionAngle = 50;

        public float currentRecoveryTime = 0;

        public float maximumAttackRange = 1.5f;
        
        public float distanceFromTarget;
        public float rotationSpeed = 15f;

        private void Start(){
          enemyRigidbody.isKinematic = false;
        }

        private void Awake(){
            
          enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
          enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
          navMeshAgent = GetComponentInChildren<NavMeshAgent>();
          navMeshAgent.enabled = true;
          enemyStats = GetComponent<EnemyStats>();
          enemyRigidbody = GetComponent<Rigidbody>();
        }

        private void Update(){
          HandleRecoveryTimer();

          isInteracting = enemyAnimatorManager.anim.GetBool("isInteracting");
          enemyRigidbody.isKinematic = false;
        } 

        private void FixedUpdate(){
                HandleStateMachine();
        } 

        private void HandleStateMachine(){
          if(currentState != null){
            State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);
               if(nextState != null){
                 SwitchToNextState(nextState);
                }
            }
        } 

        private void SwitchToNextState(State nextState){
          currentState = nextState;
        }

        private void HandleRecoveryTimer(){ 
            if(currentRecoveryTime > 0){
              currentRecoveryTime -= Time.deltaTime;
            }

            if(isPerformingAction){
              if(currentRecoveryTime <= 0){
                isPerformingAction = false;
              }
            }

        }

        #region Attacks

        private void AttackTarget(){
       /*   if(isPerformingAction){
            return;
          }



          if(currentAttack == null){
            GetNewAttack();
          }else{
              isPerformingAction = true;
              currentRecoveryTime = currentAttack.recoveryTime;
              enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
              currentAttack = null;
              
          }
*/
        }

        private void GetNewAttack(){
      /*      Vector3 targetsDirection = enemyLocomotionManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
           // enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.forward);

            int maxScore = 0;

            for(int i = 0; i<enemyAttacks.Length; ++i){
              EnemyAttackAction enemyAttackAction =  enemyAttacks[i];

              if(enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                &&enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack
                ){
                    if(viewableAngle <= enemyAttackAction.maximumAttackAngle
                      && viewableAngle >= enemyAttackAction.minimumAttackAngle
                      ){
                          maxScore  += enemyAttackAction.attackScore; 
                      }
              }
            }

              int randomValue = Random.Range(0, maxScore);
              int temporaryScore = 0;

            for(int i = 0; i<enemyAttacks.Length; ++i){
                EnemyAttackAction enemyAttackAction =  enemyAttacks[i];
                if(enemyLocomotionManager.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                            &&enemyLocomotionManager.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack
                            ){
                                if(viewableAngle <= enemyAttackAction.maximumAttackAngle
                                  && viewableAngle >= enemyAttackAction.minimumAttackAngle
                                  ){
                                    if(currentAttack != null){
                                      return;
                                    }

                                    temporaryScore += enemyAttackAction.attackScore;

                                    if(temporaryScore > randomValue){
                                      currentAttack = enemyAttackAction;
                                    }
                                  }
                }

              }*/

        }

        #endregion

    }
}