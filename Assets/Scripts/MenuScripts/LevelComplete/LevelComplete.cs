using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private LevelManager levelManager;
    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void LoadNextLevel()
    {
        levelManager.LoadNextLevel();
    }
}
