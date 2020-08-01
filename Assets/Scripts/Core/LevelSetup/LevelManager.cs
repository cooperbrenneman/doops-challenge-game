using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool CanPause;
    public static bool PlayerTurn;
    public static int Level = 1;
    
    public GameObject levelCompleteUI;
    public BoardManager boardScript;
    public LevelLoader levelLoader;
    public int CurrentLevel;


    // Start is called before the first frame update
    public virtual void Awake()
    {
        // Set current level
        Level = CurrentLevel;

        // Get a component reference to the attached BoardManager script
        //boardScript = GetComponent<BoardManager>();

        // Get the levelLoader component
        // levelLoader = FindObjectOfType<LevelLoader>();

        // Call the function to initialize the level
        InitGame();
    }

    public void InitGame()
    {
        // Call the SetupScene function of the BoardManager script
        boardScript.GenerateLevel();
        CanPause = true;
        PlayerTurn = true;
    }

    public void LevelComplete()
    {
        Debug.Log("Level Complete");
        CanPause = false;
        PlayerTurn = false;
        Time.timeScale = 0;
        ShowLevelCompleteUI();
    }

    private void ShowLevelCompleteUI()
    {
        // Display Level Complete UI
        GameObject instance = Instantiate(levelCompleteUI, new Vector3(0, 0, 0), Quaternion.identity);
        string headerText = $"Level {Level} Complete";
        FindObjectOfType<LevelCompleteUpdateText>().UpdateText(headerText);
        string buttonText = Level == 3 ? "Continue" : "Next Level";
        FindObjectOfType<LevelCompleteUpdateButtonText>().UpdateText(buttonText);
        EventSystem.current.SetSelectedGameObject(FindObjectOfType<LevelCompleteButton>().gameObject);
        
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        CanPause = false;
        PlayerTurn = false;
        levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
