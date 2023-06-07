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

    private EnemyManager enemyManager;
    private PlayerStats playerStats;

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

    private int enemyCount;
    private int playerScore = 0;

    private void Awake()
    {
        gameCanvas = GameObject.Find("Canvas");
        enemyManager = FindObjectOfType<EnemyManager>();
        playerStats = FindObjectOfType<PlayerStats>();
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
        // If the player is dead, the game time is over!
        if(playerStats.GetIsDead()){
            GameOver();
        }

        // If Game is over, restart the game
        if (isGameOver == true)
        {
            if (restartTimer <= 0)
            {
                Restart();
                restartTimer = restartDelay;
            }
            restartTimer -= Time.deltaTime;
        }

        // Otherwise, if the player is alive, rock and roll
        // Check if the player is pressing the escape key and the current scene is not the main menu
        // If not already paused then pause the game
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
        // Set the main menu timer
        mainMenuTimer += Time.deltaTime;

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level_1_intro");
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;

        playerStats.disablePlayerScript();

        gameCanvas.SetActive(false);
        SceneManager.LoadScene("GameMenu", LoadSceneMode.Additive);
        
        mainMenuTimer = 0;
        isPause = true;
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
        GameObject uiCanvas = GameObject.Find("Canvas");

        SceneManager.UnloadSceneAsync("GameMenu");

        playerStats.enablePlayerScript();

        uiCanvas.SetActive(false);
        gameCanvas.SetActive(true);

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
        
        int enemyCount = enemyManager.getEnemyCount();
        int deadEnemyCount = enemyManager.getDeadEnemyCount();
        playerScore = deadEnemyCount * 100;
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
    void OnDisable()
    {
        PlayerPrefs.SetInt("score", playerScore);
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
