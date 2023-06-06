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
        gameManager.StartGame();
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
    public void SaveGame()
    {
        gameManager.SaveGame();
    }
    public void LoadGame()
    {
        gameManager.LoadGame();
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //gameManager.LoadNextScene();
    }
}
