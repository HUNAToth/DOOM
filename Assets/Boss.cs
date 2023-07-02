using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject boss;
    private Animator animator;

    private void Awake()
    {
        //boss = GameObject.Find("Boss");
        animator = GetComponent<Animator>();
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
}
