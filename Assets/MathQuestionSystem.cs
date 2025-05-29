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
    [SerializeField] private string currentLevelName = "Level1";

    void Start()
    {
        Debug.Log("MathQuestionSystem Start in scene: " + SceneManager.GetActiveScene().name);
        // Gán currentLevelName dựa trên tên scene
        SetCurrentLevelName(); 
        if (player == null) Debug.LogError("Player không được gán! Kiểm tra Inspector của MathQuestionSystem.");
        if (questionUI == null) Debug.LogError("QuestionUI không được gán! Kiểm tra Inspector.");
        if (questionDatabase == null || questionDatabase.Count == 0) Debug.LogError("QuestionDatabase trống hoặc null!");
        if (player != null && player.StarText != null)
        {
            player.StarText.text = currentPlayerPoints.ToString();
        }
        else
        {
            Debug.LogError("Cannot update StarText: player or StarText is null!");
        }
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

    // Thêm: Hàm để gán currentLevelName dựa trên tên scene
    void SetCurrentLevelName() // Added
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Level1":
            case "Màn 1":
                currentLevelName = "Màn 1";
                break;
            case "Level_2":
            case "Màn 2":
                currentLevelName = "Màn 2";
                break;
            case "Level_3":
            case "Màn 3":
                currentLevelName = "Màn 3";
                break;
            case "Level_4":
            case "Màn 4":
                currentLevelName = "Màn 4";
                break;
            case "Level_5":
            case "Màn 5":
                currentLevelName = "Màn 5";
                break;
            case "Level_6":
            case "Màn 6":
                currentLevelName = "Màn 6";
                break;
            case "Level_7":
            case "Màn 7":
                currentLevelName = "Màn 7";
                break;
            case "Level_8":
            case "Màn 8":
                currentLevelName = "Màn 8";
                break;
            case "Level_9":
            case "Màn 9":
                currentLevelName = "Màn 9";
                break;
            case "Level_10":
            case "Màn 10":
                currentLevelName = "Màn 10";
                break;
            default:
                currentLevelName = "Màn 1"; // Mặc định nếu scene không khớp
                Debug.LogWarning($"Scene {sceneName} không khớp với màn nào, mặc định là Màn 1");
                break;
        }
        Debug.Log($"CurrentLevelName set to: {currentLevelName} in scene: {sceneName}");
    }

    public void AddPoints(int points)
    {
        Debug.Log($"AddPoints gọi với: {points} in scene: {SceneManager.GetActiveScene().name}");
        currentPlayerPoints += points;
        if (player != null)
        {
            if (player.StarText != null)
            {
                player.StarText.text = currentPlayerPoints.ToString();
                Debug.Log($"Updated StarText to: {currentPlayerPoints}");
            }
            else
            {
                Debug.LogError("player.StarText is null! Kiểm tra Inspector của XuLyVaCham.");
            }
        }
        else
        {
            Debug.LogError("player is null! Kiểm tra Inspector của MathQuestionSystem.");
        }
        Debug.Log($"Điểm hiện tại: {currentPlayerPoints}");
        PlayerPrefs.SetInt(currentLevelName + "_Score", currentPlayerPoints); // Lưu điểm level
        Debug.Log($"Đã lưu điểm {currentLevelName}: {currentPlayerPoints}");
        PlayerPrefs.Save(); // Đảm bảo ghi đĩa
        Debug.Log("Data saved to PlayerPrefs");
        UpdateTotalStats(); // Cập nhật tổng điểm

        // Kiểm tra các mốc điểm để hiển thị câu hỏi
        if (currentPlayerPoints >= 25)
        {
            Debug.Log("Đạt 25 điểm, hiển thị câu hỏi cấp 3: " + SceneManager.GetActiveScene().name);
            ShowQuestion(3);
        }
        else if (currentPlayerPoints >= 15)
        {
            Debug.Log("Đạt 15 điểm, hiển thị câu hỏi cấp 2: " + SceneManager.GetActiveScene().name);
            ShowQuestion(2);
        }
        else if (currentPlayerPoints >= 5)
        {
            Debug.Log("Đạt 5 điểm, hiển thị câu hỏi cấp 1: " + SceneManager.GetActiveScene().name);
            ShowQuestion(1);
        }
    }

    public void ShowQuestion(int difficulty)
    {
        Debug.Log($"Hiện câu hỏi cấp: {difficulty} trong màn: {SceneManager.GetActiveScene().name}");

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
            Debug.LogError("questionUI là null! Kiểm tra GameObject và Inspector.");
            return;
        }

        // bật chế độ đang làm bài, chặn va chạm
        if (player != null)
        {
            player.isAnsweringQuestion = true;
        }
        else
        {
            Debug.LogError("player is null, cannot set isAnsweringQuestion!");
        }

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

    public void CheckAnswer(string selectedAnswer)
    {
        Debug.Log($"CheckAnswer với: {selectedAnswer} in scene: {SceneManager.GetActiveScene().name}");
        bool isCorrect = selectedAnswer == currentQuestion.correctAnswer;

        if (isCorrect)
        {
            SoundEffectManager.Play("Correct");
            int reward = 2;
            currentPlayerPoints += reward;
            if (player != null && player.StarText != null)
            {
                player.StarText.text = currentPlayerPoints.ToString();
            }
            Debug.Log($"Đúng! +{reward} điểm, Điểm hiện tại: {currentPlayerPoints}");
            PlayerPrefs.SetInt(currentLevelName + "_Score", currentPlayerPoints); // Lưu điểm level
            Debug.Log($"Điểm {currentLevelName}: {currentPlayerPoints} điểm");
            PlayerPrefs.Save(); // Đảm bảo ghi đĩa
            Debug.Log("Đã lưu PlayerPrefs");
            UpdateTotalStats(); // Cập nhật tổng điểm

            questionUI.gameObject.SetActive(false);
            if (popUpSystem != null)
            {
                popUpSystem.popUpBox.SetActive(false);
            }

            // Thoát chế độ làm bài
            if (player != null)
            {
                player.isAnsweringQuestion = false;
            }
            SaveAnswerResult(currentLevelName, isCorrect);
            Debug.Log($"Số câu đúng {currentLevelName}_Correct: {PlayerPrefs.GetInt(currentLevelName + "_Correct", 0)}");

            GiveRewardItem();
        }
        else
        {
            if (player != null)
            {
                SoundEffectManager.Play("InCorrect");
                player.Hp--;
                if (player.heartText != null)
                {
                    player.heartText.text = player.Hp.ToString();
                }
                Debug.Log($"Sai! Mất 1 máu, còn lại: {player.Hp}");
            }
            else
            {
                Debug.LogError("player is null, cannot update HP!");
            }

            if (player != null && player.Hp <= 0)
            {
                Debug.Log("HP = 0, preparing to load MainMenu");
                if (SceneManager.GetActiveScene().name != "MainMenu")
                {
                    SceneManager.LoadScene("MainMenu");
                    Debug.Log("HP=0 về menu");
                }
                else
                {
                    Debug.LogWarning("Menu đã sẵn sàng.");
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
        Debug.Log($"ShowQuestionAndReturn gọi với difficulty: {difficulty} in scene: {SceneManager.GetActiveScene().name}");
        List<Quest> availableQuestions = questionDatabase.FindAll(q => q.difficulty == difficulty);
        if (availableQuestions.Count == 0)
        {
            Debug.LogWarning($"Không có câu hỏi cấp {difficulty}!");
            return null;
        }

        currentQuestion = availableQuestions[UnityEngine.Random.Range(0, availableQuestions.Count)];
        if (questionUI == null)
        {
            Debug.LogError("questionUI là null! Kiểm tra GameObject và Inspector.");
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
        PlayerPrefs.SetInt(key, current + 1); // tăng lên 1
        Debug.Log($"Saving to {key}: {current + 1}");
        PlayerPrefs.Save(); // đảm bảo ghi đĩa
        Debug.Log($"Data saved to {key}, current value: {PlayerPrefs.GetInt(key, 0)}");
        UpdateTotalStats(); // Cập nhật tổng số câu
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
        string[] levels = { "Màn 1", "Màn 2", "Màn 3", "Màn 4", "Màn 5", "Màn 6", "Màn 7", "Màn 8", "Màn 9", "Màn 10" }; 
        foreach (string level in levels)
        {
            totalCorrect += PlayerPrefs.GetInt(level + "_Correct", 0);
            totalIncorrect += PlayerPrefs.GetInt(level + "_Incorrect", 0);
        }
        totalScore = totalCorrect * 2; 
        PlayerPrefs.SetInt("TotalCorrect", totalCorrect);
        PlayerPrefs.SetInt("TotalIncorrect", totalIncorrect);
        PlayerPrefs.SetInt("TotalScore", totalScore);
        Debug.Log($"Cập nhật điểm - Đúng: {totalCorrect} câu; Sai: {totalIncorrect} câu, Tổng: {totalScore} điểm");
        PlayerPrefs.Save();
        Debug.Log("Total stats saved to PlayerPrefs");
    }
}