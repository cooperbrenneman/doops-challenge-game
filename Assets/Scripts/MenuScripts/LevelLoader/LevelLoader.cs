using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;

    public void LoadLevel(int level)
    {
        StartCoroutine(LoadScene(level));
    }

    IEnumerator LoadScene(int levelIndex)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait for animation to stop playing
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
