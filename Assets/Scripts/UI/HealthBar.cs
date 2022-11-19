using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class HealthBar : MonoBehaviour
    {
       public TextMesh  slider;

       private void Start(){
         slider = GetComponent<TextMesh>();
       }

       public void SetMaxHealth(int maxHealth){
     //   slider.maxValue = maxHealth;
        slider.text = maxHealth.ToString();
       }

       public void SetCurrentHealth(int currentHealth){
     
        slider.text = currentHealth.ToString();
       }
    }
}