using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    public Transform Player;

    public LayerMask

            whatIsGround,
            whatIsPlayer;

    //Patrolling
    public Vector3 walkPoint;

    bool walkPointSet;

    public float walkPointRange;

    //Attacking
    public GameObject projectile;

    public float timeBetweenAttacks;

    bool alreadyAttacked;

    bool isStoppedByInteraction;

    public float stopInteractionDuration;

    public float stopInteractionTimer;

    //States
    public float

            sightRange,
            attackRange;

    public bool

            playerIsInSightRange,
            playerIsInAttackRange;

    public void SetIsStoppedByInteraction(bool newValue)
    {
        isStoppedByInteraction = newValue;
    }

    public bool GetIsStoppedByInteraction()
    {
        return isStoppedByInteraction;
    }

    void Awake()
    {
        Player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isStoppedByInteraction)
        {
            stopInteractionTimer -= Time.deltaTime;
            if (stopInteractionTimer < 0)
            {
                stopInteractionTimer = 0;
                isStoppedByInteraction = false;
            }
        }
        else
        {
            //Look for player
            playerIsInSightRange =
                Physics
                    .CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerIsInAttackRange =
                Physics
                    .CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (GetComponent<EnemyStats>().currentHealth > 0)
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
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            navMeshAgent.SetDestination (walkPoint);
            var enemyanimatormanager = GetComponent<EnemyAnimatorManager>();
            enemyanimatormanager.PlayTargetAnimation("Character_Walk", false);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 2f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint =
            new Vector3(transform.position.x + randomX,
                transform.position.y,
                transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(Player.position);
        var enemyanimatormanager = GetComponent<EnemyAnimatorManager>();
        enemyanimatormanager.PlayTargetAnimation("Character_Walk", false);
    }

    private void AttackPlayer()
    {
        navMeshAgent.SetDestination(Player.position);

        //   transform.LookAt(new Vector3(Player.position.x, 0f, Player.position.y));
        if (!alreadyAttacked)
        {
            Rigidbody rb =
                Instantiate(projectile,
                transform.Find("ProjectileEmitter").transform.position,
                Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);

            EnemySoundScript soundScript =
                this.gameObject.GetComponent<EnemySoundScript>();
            soundScript.PlayAttackSound();

            //  rb.AddForce(transform.up * 0.8f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            var enemyanimatormanager = GetComponent<EnemyAnimatorManager>();
            enemyanimatormanager.PlayTargetAnimation("Attack", true);
            SetIsStoppedByInteraction(true);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
