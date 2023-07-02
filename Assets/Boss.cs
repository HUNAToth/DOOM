using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject boss;
    public EnemyStats bossStats;
    public GameObject[] enemies;
    private Animator animator;

    private void Awake()
    {
        //boss = GameObject.Find("Boss");
        animator = GetComponent<Animator>();
        bossStats = boss.GetComponent<EnemyStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Boss Triggered");
            animator.SetTrigger("start");
        }
        
    }
    
    public void SetBossActive()
    {
        boss.SetActive(true);
    }
    public void Update(){
        if(bossStats.GetCurrentHealth()!=0){
            // if the boss is at 50% health, activate the enemies
            if(bossStats.GetMaxHealth() / bossStats.GetCurrentHealth() > 2){

                foreach(GameObject enemy in enemies)
                {
                    enemy.SetActive(true);
                }
            }
        }
    }
}
