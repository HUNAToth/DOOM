using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    TextMeshProUGUI EnemyStat;

    private void Start()
    {
        EnemyStat.text =
            playerStats
                .GetLastSeenEnemy()
                .GetComponent<EnemyStats>()
                .GetCurrentHealth() +
            "%";
    }

    private void Update()
    {
        if (
            playerStats.GetLastSeenEnemy() != null &&
            playerStats
                .GetLastSeenEnemy()
                .GetComponent<EnemyStats>()
                .GetCurrentHealth() >
            0
        )
        {
            EnemyStat.text =
                playerStats
                    .GetLastSeenEnemy()
                    .GetComponent<EnemyStats>()
                    .GetCurrentHealth() +
                "%";
        }
        else
        {
            EnemyStat.text = "";
        }
    }
}
