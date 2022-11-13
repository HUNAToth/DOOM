using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SG{
public class EnemyLocomotionManager : MonoBehaviour
{

    EnemyManager enemyManager;
    EnemyAnimatorManager enemyAnimatorManager;


    public float distanceFromTarget;
    public float stoppingDistance = 1f;
    public float rotationSpeed = 15f;



    void Awake(){
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleMoveToTarget(){
        
     
    }

  
}
}
