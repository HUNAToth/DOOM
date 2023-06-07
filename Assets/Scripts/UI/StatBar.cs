using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    [SerializeField]
    private GameManager.SaveData saveData;

    [SerializeField]
    Text score;
    [SerializeField]
    Text health;
    [SerializeField]
    Text enemy;


    private void Start()
    {
        saveData = FindObjectOfType<GameManager>().GetSaveData();
        score.text = "Score: " + saveData.playerScore;
        health.text = "Health bonus: " + saveData.healthBonus;
        enemy.text = "Enemy killed: " + saveData.enemyDeadCount + "/" + saveData.enemyCount;
    }

}
