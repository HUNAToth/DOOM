using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    TextMeshProUGUI healthStat;

    private void Start()
    {
        healthStat.text = playerStats.maxHealth + "%";
    }

    private void Update()
    {
        healthStat.text = playerStats.currentHealth + "%";
    }
}
