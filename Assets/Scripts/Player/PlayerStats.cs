using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    //AnimatorHandler animatorHandler;
    private void Awake()
    {
        //  animatorHandler = GetComponentInChildren<AnimatorHandler>();
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
        return 100; //maxHealth;
    }

    private int SetMaxStaminaFromEnduranceLevel()
    {
        maxStamina = enduranceLevel * 10;
        return maxStamina;
    }

    public bool CanPickupHealthItem(int PointsRestored)
    {
        return currentHealth + PointsRestored <= 200;
    }

    public void IncreaseHealth(int value)
    {
        currentHealth = currentHealth + value;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        // animatorHandler.PlayTargetAnimation("Damage01",true);
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            //animatorHandler.PlayTargetAnimation("Death01",true);
            //handle death
        }
    }

    public void TakeStaminaDamage(int damage)
    {
        currentStamina = currentStamina - damage;
    }
}
