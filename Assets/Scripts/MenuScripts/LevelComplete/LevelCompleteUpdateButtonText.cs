using TMPro;
using UnityEngine;

public class LevelCompleteUpdateButtonText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public void UpdateText(string text)
    {
        textMeshPro.text = text;
    }
}
