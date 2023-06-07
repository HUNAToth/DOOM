using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public string activeScreenName;
        public int enemyCount;
        public int enemyDeadCount;
        public int healthBonus;
        public int playerScore;
    }
    private SaveData saveData;
    private static string filePath;

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

    // - Initial - //
    /**********************************************************************************************/
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/Save.txt";
        saveData = new SaveData();
        ReadDataFromFile();
        gameCanvas = GameObject.Find("Canvas");
        enemyManager = FindObjectOfType<EnemyManager>();
        playerStats = FindObjectOfType<PlayerStats>();
        restartTimer = restartDelay;
        Time.timeScale = 1;
        isPause = false;
    }
    // - File Write and Read - //
    /**********************************************************************************************/
    
    private void WriteDataToFile()
    {
        string jsonData = JsonConvert.SerializeObject(saveData);

        // Fájlba írás
        File.WriteAllText(filePath, jsonData);

        Debug.Log("SaveFile: " + filePath);
    }

    private void ReadDataFromFile()
    {
      if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            saveData = JsonConvert.DeserializeObject<SaveData>(jsonData);

            Debug.Log("LoadData: " + filePath);
            Debug.Log("Score: " + saveData.playerScore);
            Debug.Log("ActiveScreen: " + saveData.activeScreenName);
        }
        else
        {
            Debug.Log("File not found: " + filePath);
        }
    }

    // - Update - //
    /**********************************************************************************************/
    private void Update()
    {
        
        if(SceneManager.GetActiveScene().name != "MainMenu"){
            // If the player is dead, the game time is over!
            if(playerStats!=null && playerStats.GetIsDead() ){
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
            // Check if the player is pressing the escape key and the current scene is not the main menu
            // If not already paused then pause the game
            if (
                Input.GetKey(KeyCode.Escape) &&
                mainMenuTimer > mainMenuTime
            )
            {
                if (!isPause)
                {
                    PauseGame();
                }
            }
        }
        // Set the main menu timer
        mainMenuTimer += Time.deltaTime;

    }

    // - Menu related methods - //
    /**********************************************************************************************/
    public void StartGame()
    {
        SceneManager.LoadScene("Level_1_intro");
        playerStats.SetPlayerScore(0);
        saveData.playerScore = 0;
    }

    //Save the game
    public void SaveGame(){
        //Save the game

        string activeScreenName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        saveData.activeScreenName = activeScreenName;

        saveData.playerScore = 0;
        playerStats.SetPlayerScore(0);
    
        WriteDataToFile();
        
        ContinueGame();
        
    }

    //Load the game
    public void LoadGame(){

        try
        {
            SceneManager.LoadScene(saveData.activeScreenName);
        }
        catch (KeyNotFoundException)
        {
            Debug.Log("KeyNotFound: ActiveScreen");
        }
    }
    
    // Load the next scene
    public void LoadNextScene()
    {
        saveData.playerScore = 0;
        playerStats.SetPlayerScore(0);
        // TODO Debuging why bugy when should load main menu at the end of the game
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // - In game play methods - //
    /**********************************************************************************************/
    // Go to main menu
    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Pause the game
    public void PauseGame()
    {
        Time.timeScale = 0;

        playerStats.disablePlayerScript();
        gameCanvas.SetActive(false);

        SceneManager.LoadScene("GameMenu", LoadSceneMode.Additive);

        mainMenuTimer = 0;
        isPause = true;
    }

    // Continue the game
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

    // Game Over
    public void GameOver()
    {
        if (isGameOver == false)
        {
            playerStats.disablePlayerScript();
            isGameOver = true;
            gameOverUI.SetActive(true);
        }
    }

    // Restart the active scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Complete the level
    public void CompleteLevel()
    {
        int enemyCount = enemyManager.getEnemyCount();
        int deadEnemyCount = enemyManager.getDeadEnemyCount();
        int healthBonus = (int)(playerStats.GetCurrentHealth() / playerStats.GetMaxHealth()*100) * 1000;
        int enemyBonus = enemyCount/deadEnemyCount * 100;

        saveData.enemyCount = enemyCount;
        saveData.enemyDeadCount = deadEnemyCount;
        playerStats.SetPlayerScore(healthBonus + enemyBonus);
        playerStats.disablePlayerScript();

        saveData.healthBonus = healthBonus;
        saveData.playerScore = enemyBonus + healthBonus;
        

        gameCanvas.SetActive(false);
        isPause = true;
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

            
            WriteDataToFile();

            completeLevelUI.SetActive(true);
        }
        LoadNextScene();
    }

    // - Getters - //
    /**********************************************************************************************/
    public bool GetIsGamePause()
    {
        return isPause;
    }
    public SaveData GetSaveData()
    {
        return saveData;
    }

    // - Setters - //
    /**********************************************************************************************/
    public void setIsLevelComplete(bool _isLevelComplete)
    {
        isLevelComplete = _isLevelComplete;
    }

}
