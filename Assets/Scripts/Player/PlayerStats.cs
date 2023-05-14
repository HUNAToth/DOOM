using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    // set the max health of the player and the current health of the player
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

    // Check the player is can pickup the health item
    public bool CanPickupHealthItem(int PointsRestored)
    {
        return currentHealth + PointsRestored <= 200;
    }

    // Increase the health of the player
    public void IncreaseHealth(int value)
    {
        currentHealth = currentHealth + value;
    }

    // Take damage and logic for armor
    // if armor is 100% then first damage decrease armor by 20%
    // if armor is 0% then damage goes to health
    public void TakeDamage(int damage)
    {
        if (currentArmor <= 0)
        {
            // take damage without armor
            currentHealth -= damage;
        }
        else
        {
            // take damage with armor
            float damageReduction =
                (float) currentArmor / 100.0f * (float) damage;
            float floatDamage = (float) damage - damageReduction;

            currentHealth -= (int) Mathf.Ceil(floatDamage) / 2;

            //armor damage calculates here
            if (currentArmor != 100)
            {
                currentArmor -= (int) Mathf.Ceil(floatDamage);
                if (currentArmor <= 0)
                {
                    currentArmor = 0;
                }
            }
            else
            {
                //damage calculation, on 100 armor = 0 damage
                //on first damage decrease armor by 20%
                currentArmor -= 20;
            }
        }

        // animatorHandler.PlayTargetAnimation("Damage01",true);
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            // animatorHandler.PlayTargetAnimation("Death01",true);
            // handle death
            // get the game manager and get the player and play the die sound
            // and call the game over function
            var gameManager =
                GameObject.Find("GameManager").GetComponent<GameManager>();
            GameObject player = GameObject.Find("Player");
            player.GetComponent<PlayerMovementScript>().PlayDieSound();
            gameManager.GameOver();
        }
        else
        {
            // get the player and play the damage sound
            GameObject
                .Find("Player")
                .GetComponent<PlayerMovementScript>()
                .PlayDamageSound();
        }
    }

    // Check the player is can pickup the armor item
    public bool CanPickupArmorItem(int PointsRestored, string ArmorType)
    {
        if (currentArmor < 100)
        {
            return true;
        }
        return false;
    }

    // Increase the armor of the player
    public void IncreaseArmor(int PointsRestored, string ArmorType)
    {
        currentArmor += PointsRestored;
    }
}
