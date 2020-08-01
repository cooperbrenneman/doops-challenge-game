using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;

public class SplashScreenLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;
    public float splashScreenAppearTime = 5.2f;
    public bool skipSplashScreen = false;

    void Start()
    {
        // Skip splash screen for testing purposes
        if (skipSplashScreen)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            StartCoroutine(LoadSplashScreen());
        }
    }

    IEnumerator LoadSplashScreen()
    {
        // Wait for animation to stop playing
        yield return new WaitForSeconds(splashScreenAppearTime);

        // Load next scene
        StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadNextLevel(int levelIndex)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait for animation to stop playing
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
