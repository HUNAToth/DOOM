using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("GAMEOVER");
            GameObject
                .Find("GameManager")
                .GetComponent<GameManager>()
                .setIsLevelComplete(true);
            GameObject
                .Find("GameManager")
                .GetComponent<GameManager>()
                .CompleteLevel();
        }
    }
}
