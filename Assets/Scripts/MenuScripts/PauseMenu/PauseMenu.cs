using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject ResumeButton;

    // Update is called once per frame
    void Update()
    {
        // If player can pause and the button is pressed, then toggle pause
        // PC: Escape Xbox: Start PS4: Options 
        if (LevelManager.CanPause && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.JoystickButton9)))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        // Set Pause menu to active
        pauseMenuUI.SetActive(true);
        // Stop all player movement
        LevelManager.PlayerTurn = false;
        // Toggles Event System's current object
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ResumeButton);
        // Update Timescale to 0 to stop all movement
        Time.timeScale = 0f;
        // Toggle is Paused for input
        GameIsPaused = true;
    }

    public void Resume()
    {
        // Set Pause Menu Game Object to inactive
        pauseMenuUI.SetActive(false);
        // Update Timescale back to 1
        Time.timeScale = 1f;
        // Allow player to move
        LevelManager.PlayerTurn = true;
        // Toggle is Paused for input
        GameIsPaused = false;
    }

    public void ReloadLevel()
    {
        // Update Timescale back to 1
        Time.timeScale = 1f;
        // Load Current scene based on the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        // Update Timescale back to 1
        Time.timeScale = 1f;
        // Load Start Menu by name
        SceneManager.LoadScene("StartMenu");
    }
}
