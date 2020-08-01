using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MainMenu : MonoBehaviour
{
    public GameObject PlayButton;
    public GameObject PressAnyButtonText;
    public GameObject QuitButton;
    public GameObject Title;
    public EventSystem EventSystem;

    public Animator TitleTransition;

    public int PauseTime;

    private LevelLoader levelLoader;
    private bool CanInteract;
    private bool ShowingMenu;

    void Awake()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        CanInteract = false;
        ShowingMenu = false;
        PressAnyButtonText.SetActive(false);
        // Set menu objects to wait for input
        PauseMainMenuForInteraction();
    }

    private void Update()
    {
        // If unable to interact, do nothing
        if (!CanInteract)
        {
            return;
        }

        // If player can interact and is not currently showing menu
        if(CanInteract && !ShowingMenu)
        {
            // Wait for any key press to load menu
            if (Input.anyKeyDown)
            {
                ShowingMenu = true;
                LoadMenu();
            }
        }
    }

    private void LoadMenu()
    {
        // Turn off current press any button text
        PressAnyButtonText.SetActive(false);
        // Start coroutine to load the Main Menu text and button
        StartCoroutine(LoadMenuText());
    }

    private void PauseMainMenuForInteraction()
    {
        // Set up main menu game objects
        PressAnyButtonText.SetActive(true);
        Title.SetActive(false);
        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
        // Allow player to interact
        CanInteract = true;
    }

    IEnumerator LoadMenuText()
    {
        // Play animation
        TitleTransition.SetTrigger("Start");

        // Wait for animation to stop playing
        yield return new WaitForSeconds(PauseTime);

        // Set play button to be the current selected object
        EventSystem.current.SetSelectedGameObject(PlayButton);
    }

    public void StartNewGame()
    {
        // Load Level 1
        levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
