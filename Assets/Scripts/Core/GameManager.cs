using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject gameCanvas;

    [SerializeField]
    public GameObject completeLevelUI;

    [SerializeField]
    public GameObject completeAllLevelUI;

    [SerializeField]
    public GameObject gameOverUI;

    public bool isGameOver = false;

    public float restartDelay = 1f;

    private float mainMenuTimer = Mathf.Infinity;

    private float mainMenuTime = 1f;

    private bool isPause;

    private void Awake()
    {
        gameCanvas = GameObject.Find("Canvas");

        Time.timeScale = 1;
        isPause = false;
    }

    public bool getIsPause()
    {
        return isPause;
    }

    private void Update()
    {
        if (
            Input.GetKey(KeyCode.Escape) &&
            SceneManager.GetActiveScene().name != "MainMenu" &&
            mainMenuTimer > mainMenuTime
        )
        {
            if (!isPause)
            {
                PauseGame();
            }
        }
        mainMenuTimer += Time.deltaTime;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;

        disablePlayerScript();
        SceneManager.LoadScene("GameMenu", LoadSceneMode.Additive);
        mainMenuTimer = 0;
        isPause = true;
    }

    public void disablePlayerScript()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<GunInventory>().enabled = false;
        player
            .GetComponent<GunInventory>()
            .currentGun
            .GetComponent<GunScript>()
            .enabled = false;
        gameCanvas.SetActive(false);
    }

    public void enablePlayerScript()
    {
        GameObject player = GameObject.Find("Player");
        GameObject uiCanvas = GameObject.Find("Canvas");
        player.GetComponent<GunInventory>().enabled = true;
        player
            .GetComponent<GunInventory>()
            .currentGun
            .GetComponent<GunScript>()
            .enabled = true;
        uiCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }

    public void ContinueGame()
    {
        SceneManager.UnloadSceneAsync("GameMenu");

        enablePlayerScript();
        Time.timeScale = 1;
        mainMenuTimer = 0;
        isPause = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel()
    {
        if (
            SceneManager.sceneCountInBuildSettings - 1 ==
            SceneManager.GetActiveScene().buildIndex
        )
        {
            completeAllLevelUI.SetActive(true);
        }
        else
        {
            completeLevelUI.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            gameOverUI.SetActive(true);
        }
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
