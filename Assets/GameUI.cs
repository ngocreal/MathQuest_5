using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Update()
    {
       // scoreText.text = $"Điểm: {GameManager.Instance.GetPlayerScore()}";
    }
}