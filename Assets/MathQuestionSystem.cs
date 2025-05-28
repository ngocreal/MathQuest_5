using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class MathQuestionSystem : MonoBehaviour
{
    [SerializeField] private List<Quest> questionDatabase;
    [SerializeField] private QuestUI questionUI;
    [SerializeField] private XuLyVaCham player;
    private Chest chest;
    private int currentPlayerPoints = 0;
    private Quest currentQuestion;
    private PopUpSystem popUpSystem;

    void Start()
    {
        Debug.Log("MathQuestionSystem Start");
        if (player == null) Debug.LogError("Player không được gán!");
        if (questionUI == null) Debug.LogError("QuestionUI không được gán!");
        if (questionDatabase == null || questionDatabase.Count == 0) Debug.LogError("QuestionDatabase trống hoặc null!");
        player.StarText.text = currentPlayerPoints.ToString();
        popUpSystem = FindFirstObjectByType<PopUpSystem>();
        if (popUpSystem == null)
        {
            Debug.LogError("PopUpSystem không được tìm thấy! Kiểm tra GameObject có PopUpSystem script.");
        }
        else
        {
            Debug.Log("PopUpSystem found successfully.");
        }
        CheckResetStats(); // Kiểm tra và reset sau 7 ngày
    }

    public void AddPoints(int points)
    {
        Debug.Log($"AddPoints gọi với: {points}");
        currentPlayerPoints += points;
        player.StarText.text = currentPlayerPoints.ToString();
        Debug.Log($"Điểm hiện tại: {currentPlayerPoints}");
        PlayerPrefs.SetInt(currentLevelName + "_Score", currentPlayerPoints); // Lưu điểm level
        Debug.Log($"Saved Score for {currentLevelName}_Score: {currentPlayerPoints}");
        PlayerPrefs.Save(); // Đảm bảo ghi đĩa
        Debug.Log("Data saved to PlayerPrefs");
        UpdateTotalStats(); // Cập nhật tổng điểm

        // Kiểm tra các mốc để hiển thị câu hỏi
        if (currentPlayerPoints >= 25)
        {
            Debug.Log("Đạt 25 điểm, hiển thị câu hỏi cấp 3");
            ShowQuestion(3);
        }
        else if (currentPlayerPoints >= 15)
        {
            Debug.Log("Đạt 15 điểm, hiển thị câu hỏi cấp 2");
            ShowQuestion(2);
        }
        else if (currentPlayerPoints >= 5)
        {
            Debug.Log("Đạt 5 điểm, hiển thị câu hỏi cấp 1");
            ShowQuestion(1);
        }
    }

    public void ShowQuestion(int difficulty)
    {
        Debug.Log($"ShowQuestion gọi với difficulty: {difficulty}");

        // tránh lặp lại câu hỏi hiện tại
        List<Quest> availableQuestions = questionDatabase.FindAll(q =>
            q.difficulty == difficulty && q != currentQuestion);

        Debug.Log($"Câu hỏi cấp {difficulty}, số lượng: {availableQuestions.Count}");
        if (availableQuestions.Count == 0)
        {
            Debug.LogWarning($"Không có câu hỏi cấp {difficulty}!");
            return;
        }

        currentQuestion = availableQuestions[UnityEngine.Random.Range(0, availableQuestions.Count)];
        if (questionUI == null)
        {
            Debug.LogError("questionUI là null!");
            return;
        }

        // bật chế độ đang làm bài, chặn va chạm
        player.isAnsweringQuestion = true;

        if (popUpSystem != null)
        {
            string popupText = $"Câu hỏi cấp {difficulty}: {currentQuestion.questionText}\nĐáp án: {string.Join(", ", currentQuestion.choices)}";
            popUpSystem.PopUp(popupText);
            Debug.Log($"Popup hiển thị: {popupText}");
        }
        else
        {
            Debug.LogError("PopUpSystem null, không hiển thị popup! Kiểm tra PopUpSystem component.");
        }

        Debug.Log($"Kích hoạt QuestionUI với câu hỏi: {currentQuestion.questionText}");
        questionUI.gameObject.SetActive(true); // Đảm bảo UI đc kích hoạt
        questionUI.SetQuestion(currentQuestion);
        Debug.Log($"Hiển thị câu hỏi: {currentQuestion.questionText}");
    }

    [SerializeField] private string currentLevelName = "Level1";
    public void CheckAnswer(string selectedAnswer)
    {
        Debug.Log($"CheckAnswer với: {selectedAnswer}");
        bool isCorrect = selectedAnswer == currentQuestion.correctAnswer;

        if (isCorrect)
        {
            int reward = 2;
            currentPlayerPoints += reward;
            player.StarText.text = currentPlayerPoints.ToString();
            Debug.Log($"Đúng! +{reward} điểm, Điểm hiện tại: {currentPlayerPoints}");
            PlayerPrefs.SetInt(currentLevelName + "_Score", currentPlayerPoints); // Lưu điểm level
            Debug.Log($"Saved Score for {currentLevelName}_Score: {currentPlayerPoints}");
            PlayerPrefs.Save(); // Đảm bảo ghi đĩa
            Debug.Log("Data saved to PlayerPrefs");
            UpdateTotalStats(); // Cập nhật tổng điểm

            questionUI.gameObject.SetActive(false);
            if (popUpSystem != null)
            {
                popUpSystem.popUpBox.SetActive(false);
            }

            // Thoát chế độ làm bài
            player.isAnsweringQuestion = false;
            SaveAnswerResult(currentLevelName, isCorrect);
            Debug.Log($"Số câu đúng {currentLevelName}_Correct: {PlayerPrefs.GetInt(currentLevelName + "_Correct", 0)}");
        }
        else
        {
            player.Hp--;
            player.heartText.text = player.Hp.ToString();
            Debug.Log($"Sai! Mất 1 máu, còn lại: {player.Hp}");

            if (player.Hp <= 0)
            {
                Debug.Log("HP = 0, preparing to load MainMenu");
                if (SceneManager.GetActiveScene().name != "MainMenu") 
                {
                    SceneManager.LoadScene("MainMenu");
                    Debug.Log("Loaded MainMenu due to HP = 0");
                }
                else
                {
                    Debug.LogWarning("Already in MainMenu, no scene change needed.");
                }
                return;
            }

            questionUI.gameObject.SetActive(false);
            if (popUpSystem != null)
            {
                popUpSystem.popUpBox.SetActive(false);
            }

            // Gọi câu hỏi mới nếu còn HP, vẫn giữ currentQuestion để so sánh
            ShowQuestion(currentQuestion.difficulty);
            SaveAnswerResult(currentLevelName, isCorrect);
            Debug.Log($"Số câu sai {currentLevelName}_Incorrect: {PlayerPrefs.GetInt(currentLevelName + "_Incorrect", 0)}");
        }
    }

    public string GetSimpleQuestionText()
    {
        List<Quest> easyQuestions = questionDatabase.FindAll(q => q.difficulty == 1);
        if (easyQuestions.Count == 0)
        {
            Debug.LogWarning("Không có câu hỏi dễ để hiện popup!");
            return "Không có câu hỏi.";
        }

        Quest popupQuestion = easyQuestions[UnityEngine.Random.Range(0, easyQuestions.Count)];
        string message = $"Câu hỏi: {popupQuestion.questionText}\nĐáp án đúng: {popupQuestion.correctAnswer}";
        return message;
    }

    public Quest ShowQuestionAndReturn(int difficulty)
    {
        Debug.Log($"ShowQuestionAndReturn gọi với difficulty: {difficulty}");
        List<Quest> availableQuestions = questionDatabase.FindAll(q => q.difficulty == difficulty);
        if (availableQuestions.Count == 0)
        {
            Debug.LogWarning($"Không có câu hỏi cấp {difficulty}!");
            return null;
        }

        currentQuestion = availableQuestions[UnityEngine.Random.Range(0, availableQuestions.Count)];
        if (questionUI == null)
        {
            Debug.LogError("questionUI là null!");
            return null;
        }

        questionUI.gameObject.SetActive(true);
        questionUI.SetQuestion(currentQuestion);
        Debug.Log($"Đã kích hoạt QuestionUI với: {currentQuestion.questionText}");

        return currentQuestion;
    }

    private void GiveRewardItem()
    {
        string rewardedItemName = "Chocolate Sữa";
        int rewardedQuantity = 1;
        Sprite rewardedSprite = Resources.Load<Sprite>("Sprites/Chocolate Sữa");
        string rewardedDescription = "Restores a small amount of health.";

        menucontroller menu = GameObject.Find("UI").GetComponent<menucontroller>();
        if (menu != null)
        {
            menu.AddItem(rewardedItemName, rewardedQuantity, rewardedSprite, rewardedDescription);
            Debug.Log($"Thưởng vật phẩm: {rewardedItemName}");
        }
        else
        {
            Debug.LogError("Không tìm thấy menucontroller để thêm item!");
        }
    }

    public void SaveAnswerResult(string levelName, bool isCorrect)
    {
        string key = levelName + (isCorrect ? "_Correct" : "_Incorrect");
        int current = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, current + 1); 
        Debug.Log($"Saving to {key}: {current + 1}");
        PlayerPrefs.Save(); // đảm bảo ghi 
        Debug.Log($"Data saved to {key}, current value: {PlayerPrefs.GetInt(key, 0)}");
        UpdateTotalStats(); 
    }

    // Hàm kiểm tra và reset sau 7 ngày
    void CheckResetStats()
    {
        long lastResetTime = PlayerPrefs.GetInt("LastResetTime", 0);
        long currentTime = System.DateTime.Now.Ticks / TimeSpan.TicksPerDay; // Chuyển thành ngày
        Debug.Log($"LastResetTime: {lastResetTime}, CurrentTime: {currentTime}");
        if (currentTime - lastResetTime >= 7 || lastResetTime == 0)
        {
            PlayerPrefs.DeleteKey("TotalCorrect");
            PlayerPrefs.DeleteKey("TotalIncorrect");
            PlayerPrefs.DeleteKey("TotalScore");
            PlayerPrefs.DeleteKey("LastResetTime");
            PlayerPrefs.Save();
            Debug.Log("Đã reset stats sau 7 ngày hoặc lần đầu chơi.");
        }
        PlayerPrefs.SetInt("LastResetTime", (int)currentTime); // Cập nhật thời gian hiện tại
        Debug.Log($"Cập nhật thời gian: {currentTime}");
        PlayerPrefs.Save();
    }

    // Hàm cập nhật tổng số câu và điểm
    void UpdateTotalStats()
    {
        int totalCorrect = 0, totalIncorrect = 0, totalScore = 0;
        string[] levels = { "Level1", "Level2", "Level3", "Level4", "Level5", "Level6", "Level7", "Level8", "Level9", "Level10" };
        foreach (string level in levels)
        {
            totalCorrect += PlayerPrefs.GetInt(level + "_Correct", 0);
            totalIncorrect += PlayerPrefs.GetInt(level + "_Incorrect", 0);
            totalScore += PlayerPrefs.GetInt(level + "_Score", 0);
        }
        PlayerPrefs.SetInt("TotalCorrect", totalCorrect);
        PlayerPrefs.SetInt("TotalIncorrect", totalIncorrect);
        PlayerPrefs.SetInt("TotalScore", totalScore);
        Debug.Log($"Cập nhật điểm - Đúng: {totalCorrect} câu; Sai: {totalIncorrect} câu, Tổng: {totalScore} điểm");
        PlayerPrefs.Save();
        Debug.Log("Total stats saved to PlayerPrefs");
    }
}