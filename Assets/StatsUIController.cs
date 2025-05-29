using UnityEngine;
using TMPro;

public class StatsUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI correctAnswersText;
    [SerializeField] private TextMeshProUGUI incorrectAnswersText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TMP_Dropdown levelDropdown;
    [SerializeField] private TextMeshProUGUI levelCorrectText;
    [SerializeField] private TextMeshProUGUI levelIncorrectText;
    [SerializeField] private TextMeshProUGUI levelScoreText;
    [SerializeField] private GameObject menudrop; 

    void Start()
    {
        Debug.Log("StatsUIController Start called in scene: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        SetupDropdown();
        UpdateTotalStatsDisplay(); // tổng stats
        UpdateLevelStatsDisplay(); // stats theo màn mặc định
        if (menudrop != null) menudrop.SetActive(false); 
    }

    void SetupDropdown()
    {
        if (levelDropdown != null)
        {
            levelDropdown.ClearOptions();
            string[] levels = { "Chọn màn", "Màn 1", "Màn 2", "Màn 3", "Màn 4", "Màn 5", "Màn 6", "Màn 7", "Màn 8", "Màn 9", "Màn 10" };
            levelDropdown.AddOptions(new System.Collections.Generic.List<string>(levels));
            levelDropdown.onValueChanged.AddListener(delegate { UpdateLevelStatsDisplay(); });
            levelDropdown.value = 0; // Default to "Select Level"
            
            if (levelDropdown.captionText != null)
            {
                levelDropdown.captionText.fontSize = 60; 
                levelDropdown.captionText.color = new Color32(0, 0, 255, 255); 
            }
            if (levelDropdown.template != null)
            {
                TMP_Text[] itemTexts = levelDropdown.template.GetComponentsInChildren<TMP_Text>();
                foreach (TMP_Text itemText in itemTexts)
                {
                    itemText.fontSize = 20; 
                    itemText.color = new Color32(0, 0, 255, 255); 
                }
            }
        }
        else
        {
            Debug.LogError("levelDropdown is null! Kiểm tra Inspector.");
        }
    }

    void UpdateTotalStatsDisplay()
    {
        int totalCorrect = PlayerPrefs.GetInt("TotalCorrect", 0);
        int totalIncorrect = PlayerPrefs.GetInt("TotalIncorrect", 0);
        int totalScore = PlayerPrefs.GetInt("TotalScore", 0);

        Debug.Log($"Đã lấy db - Tổng: Số câu đúng: {totalCorrect}, Số câu sai: {totalIncorrect}, Tổng điểm: {totalScore}");

        if (correctAnswersText != null)
        {
            correctAnswersText.text = $"{totalCorrect}";
            Debug.Log($"Hiển thị Tổng: Số câu đúng: {totalCorrect}");
        }
        else
            Debug.LogError("correctAnswersText is null!");

        if (incorrectAnswersText != null)
        {
            incorrectAnswersText.text = $"{totalIncorrect}";
            Debug.Log($"Hiển thị Tổng: Số câu sai: {totalIncorrect}");
        }
        else
            Debug.LogError("incorrectAnswersText is null!");

        if (scoreText != null)
        {
            scoreText.text = $"{totalScore}";
            Debug.Log($"Hiển thị Tổng: Tổng điểm: {totalScore}");
        }
        else
            Debug.LogError("scoreText is null!");
    }

    void UpdateLevelStatsDisplay()
    {
        string selectedLevel = levelDropdown != null && levelDropdown.value > 0 ? levelDropdown.options[levelDropdown.value].text : null;
        // Thêm: Kiểm tra và hiển thị/ẩn menudrop dựa trên lựa chọn
        if (menudrop != null)
        {
            menudrop.SetActive(!string.IsNullOrEmpty(selectedLevel) && selectedLevel != "Chọn màn");
        }
        if (string.IsNullOrEmpty(selectedLevel) || selectedLevel == "Chọn màn")
        {
            if (levelCorrectText != null) levelCorrectText.text = "-";
            if (levelIncorrectText != null) levelIncorrectText.text = "-";
            if (levelScoreText != null) levelScoreText.text = "-";
            return;
        }

        int correct = PlayerPrefs.GetInt(selectedLevel + "_Correct", 0);
        int incorrect = PlayerPrefs.GetInt(selectedLevel + "_Incorrect", 0);
        int score = PlayerPrefs.GetInt(selectedLevel + "_Score", 0);

        Debug.Log($"Đã lấy db - {selectedLevel}: Số câu đúng: {correct}, Số câu sai: {incorrect}, Điểm: {score}");

        if (levelCorrectText != null)
        {
            levelCorrectText.text = $"{correct}";
            Debug.Log($"Hiển thị {selectedLevel}: Số câu đúng: {correct}");
        }
        else
            Debug.LogError("levelCorrectText is null!");

        if (levelIncorrectText != null)
        {
            levelIncorrectText.text = $"{incorrect}";
            Debug.Log($"Hiển thị {selectedLevel}: Số câu sai: {incorrect}");
        }
        else
            Debug.LogError("levelIncorrectText is null!");

        if (levelScoreText != null)
        {
            levelScoreText.text = $"{score}";
            Debug.Log($"Hiển thị {selectedLevel}: Điểm: {score}");
        }
        else
            Debug.LogError("levelScoreText is null!");
    }
}