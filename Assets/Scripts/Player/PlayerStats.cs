using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class PlayerStats : CharacterStats
    {
     

        public HealthBar healthBar;
        public StaminaBar staminaBar;

        //AnimatorHandler animatorHandler;


        private void Awake(){
          //  animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }


        // Start is called before the first frame update
        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);


         /*   maxStamina = SetMaxStaminaFromEnduranceLevel();
            currentStamina = maxStamina;
            staminaBar.SetMaxStamina(maxStamina);*/
        }

        private int SetMaxHealthFromHealthLevel(){
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

          private int SetMaxStaminaFromEnduranceLevel(){
            maxStamina = enduranceLevel * 10;
            return maxStamina;
        }

        public void IncreaseHealth(int value){
            currentHealth = currentHealth + value;
            healthBar.SetCurrentHealth(currentHealth);
        }

        public void TakeDamage(int damage){
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

           // animatorHandler.PlayTargetAnimation("Damage01",true);

            if(currentHealth <=0){
                currentHealth = 0;

            //animatorHandler.PlayTargetAnimation("Death01",true);
            //handle death
            }
        }


        public void TakeStaminaDamage(int damage){
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
        }
    }
}