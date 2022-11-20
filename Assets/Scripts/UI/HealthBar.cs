using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace SG
{
    public class HealthBar : MonoBehaviour
    {

        [SerializeField] private PlayerStats playerStats;
        [SerializeField] TextMeshProUGUI healthStat;

        private void Start(){
          healthStat.text = playerStats.maxHealth+"%";
        }

        private void Update(){
          healthStat.text = playerStats.currentHealth+"%";
        }
    }
}