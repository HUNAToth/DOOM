using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    private const string fileName = "Save.txt";
    private GameObject gameCanvas;

    [SerializeField]
    public GameObject completeLevelUI;

    [SerializeField]
    public GameObject completeAllLevelUI;

    [SerializeField]
    public GameObject gameOverUI;

    public bool isGameOver = false;

    public float restartDelay = 5f;

    private float restartTimer = 5f;

    private float mainMenuTimer = Mathf.Infinity;

    private float mainMenuTime = 1f;

    private bool isPause;

    private bool isLevelComplete = false;

    private void Awake()
    {
        gameCanvas = GameObject.Find("Canvas");
        restartTimer = restartDelay;
        Time.timeScale = 1;
        isPause = false;
    }

    public bool getIsPause()
    {
        return isPause;
    }

    public void setIsLevelComplete(bool _isLevelComplete)
    {
        isLevelComplete = _isLevelComplete;
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
        if (isGameOver == true)
        {
            if (restartTimer <= 0)
            {
                Restart();
                restartTimer = restartDelay;
            }
            restartTimer -= Time.deltaTime;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1_intro");
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
    //This method will be called when the player click the save button
    //It will save the screen name and the player position
    //The screen name will be used to load the screen
    //The player position will be used to load the player position
    //The data will be saved SaveData.txt file in JSON format

    public void SaveGame(){
        //Save the game
        
        string activeScreenName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(filePath, activeScreenName);
        ContinueGame();
        
    }

    public void ContinueGame()
    {
        SceneManager.UnloadSceneAsync("GameMenu");
        enablePlayerScript();
        Time.timeScale = 1;
        mainMenuTimer = 0;
        isPause = false;
    }

    public void LoadGame(){
        //Load the game
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        string activeScreenName = File.ReadAllText(filePath);
        SceneManager.LoadScene(activeScreenName);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel()
    {
        Debug.Log("CompleteLevel");
        if (
            SceneManager.sceneCountInBuildSettings - 1 ==
            SceneManager.GetActiveScene().buildIndex
        )
        {
            completeAllLevelUI.SetActive(true);
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            completeLevelUI.SetActive(true);
            LoadNextScene();
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

   public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
