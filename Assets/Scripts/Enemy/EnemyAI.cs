using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private EnemyStats enemyStats;

    private EnemyAnimatorManager enemyAnimatorManager;

    public NavMeshAgent navMeshAgent;

    private Transform Player;

    public LayerMask

            whatIsGround,
            whatIsPlayer;

    //Patrolling
    private Vector3 destinationPoint;

    bool isDestinationSet;
    float destinationTimer;


    //Attacking
    public GameObject projectile;

    bool alreadyAttacked;

    //States
    protected bool

            playerIsInSightRange,
            playerIsInAttackRange;

    void Awake()
    {
        Player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<EnemyStats>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
    }

    void Update()
    {
        //Look for player
        playerIsInSightRange =
            Physics
                .CheckSphere(transform.position,
                enemyStats.sightRange,
                whatIsPlayer);
        playerIsInAttackRange =
            Physics
                .CheckSphere(transform.position,
                enemyStats.attackRange,
                whatIsPlayer);

        if (enemyStats.GetCurrentHealth() > 0)
        {
            if (!playerIsInSightRange && !playerIsInAttackRange)
            {
                Debug.Log("Patrolling");
                Patrolling();
            }
            if (
                (enemyStats.GetLastSeenEnemy() != null && !playerIsInAttackRange) || (playerIsInSightRange && !playerIsInAttackRange) ||( enemyStats.GetLastSeenEnemy() != null && playerIsInSightRange && alreadyAttacked)
            )
            {
                Debug.Log("ChasePlayer");
                ChasePlayer();
            }
            if ((enemyStats.GetLastSeenEnemy() != null && playerIsInAttackRange ) || (playerIsInSightRange && playerIsInAttackRange)) 
            {
                Debug.Log("AttackPlayer");
                AttackPlayer();
            }
           
         
        
        }
    }

    private void SearchDestinationPoint()
    {
        float randomZ =
            Random.Range(-enemyStats.patrolRange, enemyStats.patrolRange);
        float randomX =
            Random.Range(-enemyStats.patrolRange, enemyStats.patrolRange);

        destinationPoint =
            new Vector3(transform.position.x + randomX,
                transform.position.y,
                transform.position.z + randomZ);

        if (Physics.Raycast(destinationPoint, -transform.up, 2f, whatIsGround))
        {
            isDestinationSet = true;
        }
    }

     private void Patrolling()
    {
        // Debug.Log("Patrolling");
        if (!isDestinationSet)
        {

            SearchDestinationPoint();
        }
        if (isDestinationSet)
        {
            navMeshAgent.SetDestination (destinationPoint);
        }
        
        enemyAnimatorManager.SetWalk(true);

        Vector3 distanceToDestinationPoint =
            transform.position - destinationPoint;

        Debug.Log(distanceToDestinationPoint.magnitude);

        if (distanceToDestinationPoint.magnitude < 2f)
        {
            isDestinationSet = false;
        }
    }

    // Chase player
    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(Player.position);
        enemyAnimatorManager.SetWalk(true);
    }

    private void AttackPlayer()
    {
        navMeshAgent.SetDestination(Player.position);

        if (!alreadyAttacked)
        {
            if (enemyStats.EnemyType == "Ranged")
            {
                Rigidbody rb =
                    Instantiate(projectile,
                    transform.Find("ProjectileEmitter").transform.position,
                    Quaternion.identity).GetComponent<Rigidbody>();
                rb
                    .AddForce(transform.forward * enemyStats.fireballSpeed,
                    ForceMode.Impulse);
            }
            else if (enemyStats.EnemyType == "Melee")
            {
                GameObject Player;
                Player = GameObject.Find("Player");
                Player
                    .GetComponent<PlayerStats>()
                    .TakeDamage(enemyStats.attackDamage);
            }

            EnemySoundScript soundScript =
                this.gameObject.GetComponent<EnemySoundScript>();
            soundScript.PlayAttackSound();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), enemyStats.attackCooldown);
            enemyAnimatorManager.PlayAttack();
        }
    }

    // Reset attack after cooldown
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
