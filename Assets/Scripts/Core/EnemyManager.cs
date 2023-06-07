using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to manage all enemies in the scene
public class EnemyManager : MonoBehaviour
{
    public GameObject[] activeEnemyList;
    //public GameObject[] inactiveEnemyList;

    private int enemyCount;


    void Awake()
    {
        activeEnemyList = GameObject.FindGameObjectsWithTag("Dummie");
        enemyCount = activeEnemyList.Length;
    }

    void Update()
    {
       foreach (GameObject enemy in activeEnemyList)
        {
            if (enemy.GetComponent<EnemyStats>().GetIsDead())
            {
                enemy.GetComponent<EnemyAI>().enabled = false;
                //inactiveEnemyList.Add(enemy);
                //activeEnemyList.Remove(enemy);

                //enemy.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }

    public int getEnemyCount()
    {
        return enemyCount;
    }

    public int getDeadEnemyCount(){
            int deadEnemyCount = 0;
            foreach (GameObject enemy in activeEnemyList)
            {
                if (enemy.GetComponent<EnemyStats>().GetIsDead())
                {
                    deadEnemyCount++;
                }
            }
            return deadEnemyCount;
    }


}
