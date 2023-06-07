using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, string> keyValuePairs;
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
        keyValuePairs = new Dictionary<string, string>();
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
        StreamWriter writer = new StreamWriter(filePath, false);
        foreach (KeyValuePair<string, string> pair in keyValuePairs)
        {
            writer.WriteLine(pair.Key + ":" + pair.Value);
        }
        writer.Close();
    }

    private void ReadDataFromFile()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    string key = parts[0];
                    string value = parts[1];

                    keyValuePairs.Add(key, value);
                }
        }
        else
        {
            Debug.Log("FileNotFound: " + filePath);
        }
    }

    // - Update - //
    /**********************************************************************************************/
    private void Update()
    {
        
        if(SceneManager.GetActiveScene().name != "MainMenu"){
            // If the player is dead, the game time is over!
            if(playerStats.GetIsDead() ){
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
    }

    //Save the game
    public void SaveGame(){
        //Save the game
        
        string activeScreenName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        keyValuePairs.Add("ActiveScreen", activeScreenName);
        //Save Dictionary to file
        WriteDataToFile();
        //string filePath = Path.Combine(Application.persistentDataPath, fileName);
        //File.WriteAllText(filePath, activeScreenName);
        
        ContinueGame();
        
    }

    //Load the game
    public void LoadGame(){

        ReadDataFromFile();
        try
        {
            string activeScreenName = keyValuePairs["ActiveScreen"];
            SceneManager.LoadScene(activeScreenName);
        }
        catch (KeyNotFoundException)
        {
            Debug.Log("KeyNotFound: ActiveScreen");
        }
    }
    
    // Load the next scene
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        
        playerStats.SetPlayerScore(enemyManager.getDeadEnemyCount() * 100);
        playerStats.disablePlayerScript();
        gameCanvas.SetActive(false);
        //mainMenuTimer = 0;
        isPause = true;
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
            keyValuePairs.Add("Score", playerStats.GetPlayerScore().ToString());
            WriteDataToFile();

            completeLevelUI.SetActive(true);
            LoadNextScene();
        }
    }

    // - Getters - //
    /**********************************************************************************************/
    public bool GetIsGamePause()
    {
        return isPause;
    }

    // - Setters - //
    /**********************************************************************************************/
    public void setIsLevelComplete(bool _isLevelComplete)
    {
        isLevelComplete = _isLevelComplete;
    }

}
