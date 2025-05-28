using UnityEngine;
using TMPro;

public class StatsUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI correctAnswersText; 
    [SerializeField] private TextMeshProUGUI incorrectAnswersText; 
    [SerializeField] private TextMeshProUGUI scoreText; 

    void Start()
    {
        Debug.Log("StatsUIController Start called");
        UpdateStatsDisplay(); // Hiển thị tổng thống kê
    }

    void UpdateStatsDisplay()
    {
        int totalCorrect = PlayerPrefs.GetInt("TotalCorrect", 0);
        int totalIncorrect = PlayerPrefs.GetInt("TotalIncorrect", 0);
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);

        Debug.Log($"Retrieved from PlayerPrefs - TotalCorrect: {totalCorrect}, TotalIncorrect: {totalIncorrect}, TotalScore: {totalScore}");

        if (correctAnswersText != null)
        {
            correctAnswersText.text = $"Số câu đúng: {totalCorrect}";
            Debug.Log($"Displayed: Số câu đúng: {totalCorrect}");
        }
        else
            Debug.LogError("correctAnswersText is null!");

        if (incorrectAnswersText != null)
        {
            incorrectAnswersText.text = $"Số câu sai: {totalIncorrect}";
            Debug.Log($"Displayed: Số câu sai: {totalIncorrect}");
        }
        else
            Debug.LogError("incorrectAnswersText is null!");

        if (scoreText != null)
        {
            scoreText.text = $"Tổng điểm: {totalScore}";
            Debug.Log($"Displayed: Tổng điểm: {totalScore}");
        }
        else
            Debug.LogError("scoreText is null!");
    }
}