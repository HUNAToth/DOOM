using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    TextMeshProUGUI armorStat;

    private void Start()
    {
        armorStat.text = playerStats.maxArmor + "%";
    }

    private void Update()
    {
        armorStat.text = playerStats.currentArmor + "%";
    }
}
