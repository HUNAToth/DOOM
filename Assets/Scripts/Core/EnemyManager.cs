using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to manage all enemies in the scene
public class EnemyManager : MonoBehaviour
{
    public GameObject[] activeEnemyList;

    private int enemyCount;
    private int availableEnemyCount = 0;
    private int deadEnemyCount = 0;


    void Awake()
    {
        activeEnemyList = GameObject.FindGameObjectsWithTag("Dummie");
        enemyCount = activeEnemyList.Length;
        availableEnemyCount = enemyCount;
    }

    void Update()
    {
       foreach (GameObject enemy in activeEnemyList)
        {
            if (enemy.GetComponent<EnemyStats>().GetIsDead() && enemy.GetComponent<EnemyAI>().enabled==true )
            {
                enemy.GetComponent<EnemyAI>().enabled = false;
            }
        }
        deadEnemyCount = getDeadEnemyCount();
        availableEnemyCount = enemyCount - deadEnemyCount;
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

    public void setAllEnemyEnable(bool enable){
        foreach (GameObject enemy in activeEnemyList)
        {
            enemy.GetComponent<EnemyAI>().enabled = enable;
        }
    }
    
    // Get the available enemy count
    public int GetAvailableEnemyCount()
    {
        return availableEnemyCount;
    }

    // Get the dead enemy count
    public int GetDeadEnemyCount()
    {
        return deadEnemyCount;
    }
    // Set the dead enemy count

}
