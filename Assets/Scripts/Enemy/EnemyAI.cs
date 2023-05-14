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
        /* if (isStoppedByInteraction)
        {
            stopInteractionTimer -= Time.deltaTime;
            if (stopInteractionTimer < 0)
            {
                stopInteractionTimer = 0;
                isStoppedByInteraction = false;
            }
        }
        else
        {*/
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
                Patrolling();
            }
            else if (playerIsInSightRange && !playerIsInAttackRange)
            {
                ChasePlayer();
            }
            else if (playerIsInSightRange && playerIsInAttackRange)
            {
                AttackPlayer();
            }
        }
        //}
    }

    private void Patrolling()
    {
        enemyAnimatorManager.PlayWalk();
        if (!isDestinationSet)
        {
            SearchDestinationPoint();
        }
        if (isDestinationSet)
        {
            navMeshAgent.SetDestination (destinationPoint);
        }

        Vector3 distanceTodestinationPoint =
            transform.position - destinationPoint;
        if (distanceTodestinationPoint.magnitude < 2f)
        {
            isDestinationSet = false;
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

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(Player.position);
        enemyAnimatorManager.PlayWalk();
    }

    private void AttackPlayer()
    {
        navMeshAgent.SetDestination(Player.position);

        //   transform.LookAt(new Vector3(Player.position.x, 0f, Player.position.y));
        if (!alreadyAttacked)
        {
            if (enemyStats.EnemyType == "Ranged")
            {
                Rigidbody rb =
                    Instantiate(projectile,
                    transform.Find("ProjectileEmitter").transform.position,
                    Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 1f, ForceMode.Impulse);
            }
            else if (enemyStats.EnemyType == "Melee")
            {
                //spherrel megnézni playert, őt sebezni x-el
                GameObject Player;
                Player = GameObject.Find("Player");
                Player
                    .GetComponent<PlayerStats>()
                    .TakeDamage(enemyStats.attackDamage);
            }

            EnemySoundScript soundScript =
                this.gameObject.GetComponent<EnemySoundScript>();
            soundScript.PlayAttackSound();

            //  rb.AddForce(transform.up * 0.8f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), enemyStats.attackCooldown);
            enemyAnimatorManager.PlayAttack();
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    /*   private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyStats.attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyStats.sightRange);
    }*/
}
