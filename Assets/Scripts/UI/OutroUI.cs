using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroUi : MonoBehaviour
{
    public int playerScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        playerScore  =  PlayerPrefs.GetInt("score");
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerScore);
    }
}
