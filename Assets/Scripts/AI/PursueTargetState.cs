using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG{
    public class PursueTargetState : State
    {

        public CombatStanceState combatStanceState;  
        
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager){
            //chase target

        if(enemyManager.isPerformingAction){
                return this;
            }
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;

            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if( enemyManager.distanceFromTarget > enemyManager.maximumAttackRange){       
                       enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }

            HandleRotateTowardsTarget(enemyManager);

            //ez a sor random yutubról van elvileg megoldja a magasságban ragadás hibát
            transform.position = new Vector3(transform.position.x, enemyManager.navMeshAgent.transform.position.y, transform.position.z);

            enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
            enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;
                //switch to combat stance if  within attack range
                if(enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange){
                    return combatStanceState;
                }else{
                    return this;
                }
     }
//https://www.youtube.com/watch?v=g43lohNVF9M&ab_channel=SebastianGraves
//COMBAT STATE JÖN
     private void HandleRotateTowardsTarget(EnemyManager enemyManager){
        if(enemyManager.isPerformingAction){
             Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
             direction.y = 0;
             direction.Normalize();

            if(direction == Vector3.zero){
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed /Time.deltaTime);


        }else{
            Debug.Log("Rptation else");
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigidbody.velocity = targetVelocity;
            
            enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed /Time.deltaTime);
        }         
    }
    }

    
}