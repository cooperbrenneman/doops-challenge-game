using UnityEngine;

public class CompleteGame : MonoBehaviour
{
    private LevelLoader levelLoader;
    void Awake()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void LoadMainMenu()
    {
        // Hardcoding to 1, which is the build index of the main menu
        levelLoader.LoadLevel(1);
    }

}
