using TMPro;
using UnityEngine;

public class LevelCompleteUpdateText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public void UpdateText(string text)
    {
        textMeshPro.text = text;
    }
}
