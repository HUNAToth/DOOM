using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth = currentHealth - damage;
            EnemySoundScript soundScript =
                this.gameObject.GetComponent<EnemySoundScript>();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death01");
                soundScript.PlayDieSound();
                //TODO deative during animation
            }
            else
            {
                soundScript.PlayDamageSound();
                animator.Play("Damage01");

                EnemyAI enemyAI = GetComponent<EnemyAI>();
                enemyAI.SetIsStoppedByInteraction(true);
            }
        }
    }
}
