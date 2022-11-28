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
        Debug.Log("flat damage: " + damage);

        Debug
            .Log("reduced damage: " +
            (
            (float) damage - ((float) damage * ((float) currentArmor / 100.0f))
            ));

        Debug.Log("current armor: " + currentArmor);

        currentHealth -=
            (
            int
            )((float) damage -
            ((float) damage * ((float) currentArmor / 100.0f)));

        if (currentArmor != 100)
        {
            currentArmor -=
                (
                int
                )((float) damage -
                ((float) damage * ((float) currentArmor / 100.0f)));
            if (currentArmor <= 0)
            {
                currentArmor = 0;
            }

            Debug.Log("reduced armor: " + currentArmor);
        }
        else
        {
            //TODO currentarmor csökkentése ha már 100%ot véd
            currentArmor -= 20;
        }

        // animatorHandler.PlayTargetAnimation("Damage01",true);
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            //animatorHandler.PlayTargetAnimation("Death01",true);
            //handle death
            var gameManager =
                GameObject.Find("GameManager").GetComponent<GameManager>();
            GameObject player = GameObject.Find("Player");
            player.GetComponent<PlayerMovementScript>().PlayDieSound();
            gameManager.GameOver();
        }
        else
        {
            GameObject
                .Find("Player")
                .GetComponent<PlayerMovementScript>()
                .PlayDamageSound();
        }
    }
}
