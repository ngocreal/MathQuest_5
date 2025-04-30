using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public QuestionUI questionUI;
    public XuLyVaCham player;

    private int playerScore = 0;
    private int currentLevel = 1; // Màn chơi hiện tại
    private bool isQuestionActive = false;
    private bool hasShownQuestionAt18 = false;
    private bool hasShownQuestionAt36 = false;

    private int correctAnswersThisLevel = 0; // Số câu đúng trong màn hiện tại
    private int wrongAnswersThisLevel = 0; // Số câu sai trong màn hiện tại

    private void Awake()
    {
        Debug.Log("GameManager Awake được gọi");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Tải dữ liệu đã lưu
        LoadPlayerData();
    }

    private void Start()
    {
        Debug.Log($"Điểm số khởi tạo: {playerScore}, Màn: {currentLevel}");
    }

    private void Update()
    {
        if (!isQuestionActive)
        {
            if (!hasShownQuestionAt18 && playerScore >= 18)
            {
                hasShownQuestionAt18 = true;
                DisplayQuestionAtMilestone(2);
            }
            else if (!hasShownQuestionAt36 && playerScore >= 36)
            {
                hasShownQuestionAt36 = true;
                DisplayQuestionAtMilestone(3);
            }
        }
    }

    public void TriggerQuestion(GameObject triggerObject)
    {
        Debug.Log("TriggerQuestion được gọi với triggerObject: " + (triggerObject != null ? triggerObject.name : "null"));
        if (isQuestionActive)
        {
            Debug.Log("isQuestionActive = true, không hiển thị câu hỏi mới.");
            return;
        }

        if (triggerObject != null && triggerObject.CompareTag("enemy"))
        {
            triggerObject.GetComponent<Enemy_behavior>().Defeat();
            player.DefeatEnemy();
            AddScore(5);
        }
        else if (triggerObject != null && triggerObject.CompareTag("Vang"))
        {
            player.CollectItem();
            AddScore(2);
        }
    }

    private void DisplayQuestionAtMilestone(int difficulty)
    {
        Debug.Log($"Đạt mốc điểm {playerScore}, hiển thị câu hỏi độ khó {difficulty}");
        isQuestionActive = true;
        player.playerController.canMove = false;

        Question question = QuestionLoader.Instance.GetRandomQuestion(difficulty);
        if (question != null)
        {
            Debug.Log($"Câu hỏi được chọn: {question.questionText}");
            questionUI.DisplayNewQuestion(question);
        }
        else
        {
            Debug.LogError("Không thể tải câu hỏi từ QuestionLoader!");
            isQuestionActive = false;
            player.playerController.canMove = true;
        }
    }

    public void OnAnswerSubmitted(bool isCorrect, int difficulty)
    {
        Debug.Log($"Trả lời câu hỏi: Đúng = {isCorrect}, Độ khó = {difficulty}");
        if (isCorrect)
        {
            correctAnswersThisLevel++;
            AddScore(5);
            Debug.Log($"Điểm số mới: {playerScore}");
            questionUI.HideQuestionPanel();
            isQuestionActive = false;
            player.playerController.canMove = true;

            if (player.currentTriggerObject != null)
            {
                if (player.currentTriggerObject.CompareTag("enemy"))
                {
                    player.currentTriggerObject.GetComponent<Enemy_behavior>().Defeat();
                    player.DefeatEnemy();
                }
                else if (player.currentTriggerObject.CompareTag("Vang"))
                {
                    player.CollectItem();
                }
            }
        }
        else
        {
            wrongAnswersThisLevel++;
            questionUI.HideQuestionPanel();
            isQuestionActive = false;
            player.playerController.canMove = true;

            if (player.currentTriggerObject != null)
            {
                if (player.currentTriggerObject.CompareTag("enemy"))
                {
                    player.TakeDamage();
                    if (player.Hp > 0)
                    {
                        TriggerQuestion(null);
                    }
                }
                else if (player.currentTriggerObject.CompareTag("Vang"))
                {
                    player.SkipItem();
                }
            }
        }

        // Lưu dữ liệu sau mỗi câu trả lời
        SaveAnswerData();
    }

    private int GetDifficultyFromScore(int score)
    {
        Debug.Log($"Tính độ khó với điểm số: {score}");
        if (score < 20) return 1;
        if (score < 50) return 2;
        return 3;
    }

    public void AddScore(int points)
    {
        playerScore += points;
        Debug.Log($"Điểm số mới: {playerScore}");
        PlayerPrefs.SetInt("CurrentScore", playerScore);
        PlayerPrefs.Save();
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void CompleteLevel(int level)
    {
        Debug.Log($"Hoàn thành màn {level}");
        currentLevel = level;
        PlayerPrefs.SetInt("LastPlayedLevel", currentLevel);
        SaveAnswerData();
        correctAnswersThisLevel = 0; // Reset sau khi hoàn thành màn
        wrongAnswersThisLevel = 0;
    }

    private void SaveAnswerData()
    {
        int week = GetCurrentWeekNumber();
        PlayerPrefs.SetInt($"CorrectAnswersPerLevel_{currentLevel}_{week}", correctAnswersThisLevel);
        PlayerPrefs.SetInt($"WrongAnswersPerLevel_{currentLevel}_{week}", wrongAnswersThisLevel);
        PlayerPrefs.SetInt("LastPlayedWeek", week);
        PlayerPrefs.Save();
        Debug.Log($"Lưu dữ liệu: Màn {currentLevel}, Tuần {week}, Đúng: {correctAnswersThisLevel}, Sai: {wrongAnswersThisLevel}");
    }

    private void LoadPlayerData()
    {
        playerScore = PlayerPrefs.GetInt("CurrentScore", 0);
        currentLevel = PlayerPrefs.GetInt("LastPlayedLevel", 1);
        int week = GetCurrentWeekNumber();
        correctAnswersThisLevel = PlayerPrefs.GetInt($"CorrectAnswersPerLevel_{currentLevel}_{week}", 0);
        wrongAnswersThisLevel = PlayerPrefs.GetInt($"WrongAnswersPerLevel_{currentLevel}_{week}", 0);
        Debug.Log($"Tải dữ liệu: Điểm: {playerScore}, Màn: {currentLevel}, Đúng: {correctAnswersThisLevel}, Sai: {wrongAnswersThisLevel}");
    }

    private int GetCurrentWeekNumber()
    {
        DateTime now = DateTime.Now;
        int weekNumber = (now.DayOfYear - 1) / 7 + 1;
        return weekNumber;
    }

    public int GetCorrectAnswers(int level, int week)
    {
        return PlayerPrefs.GetInt($"CorrectAnswersPerLevel_{level}_{week}", 0);
    }

    public int GetWrongAnswers(int level, int week)
    {
        return PlayerPrefs.GetInt($"WrongAnswersPerLevel_{level}_{week}", 0);
    }

    public int GetLastPlayedLevel()
    {
        return PlayerPrefs.GetInt("LastPlayedLevel", 1);
    }

    public int GetLastPlayedWeek()
    {
        return PlayerPrefs.GetInt("LastPlayedWeek", GetCurrentWeekNumber());
    }
}