using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartGame()
    {
        Debug.Log("doin something");
        SceneManager.LoadScene("Level_1");
    }

    public void ContinueGame()
    {
        gameManager.ContinueGame();
    }

    public void RestartLevel()
    {
        gameManager.Restart();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void goToMainManu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
