using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MathQuestionSystem : MonoBehaviour
{
    [SerializeField] private List<Quest> questionDatabase;
    [SerializeField] private QuestUI questionUI;
    [SerializeField] private XuLyVaCham player;
    private int currentPlayerPoints = 0;
    private Quest currentQuestion;

    void Start()
    {
        Debug.Log("MathQuestionSystem Start");
        if (player == null) Debug.LogError("Player không được gán!");
        if (questionUI == null) Debug.LogError("QuestionUI không được gán!");
        if (questionDatabase == null || questionDatabase.Count == 0) Debug.LogError("QuestionDatabase trống hoặc null!");
        player.StarText.text = currentPlayerPoints.ToString();
    }

    public void AddPoints(int points)
    {
        Debug.Log($"AddPoints gọi với: {points}");
        currentPlayerPoints += points;
        player.StarText.text = currentPlayerPoints.ToString();
        Debug.Log($"Điểm hiện tại: {currentPlayerPoints}");

        if (currentPlayerPoints >= 25)
            ShowQuestion(3);
        else if (currentPlayerPoints >= 15)
            ShowQuestion(2);
        else if (currentPlayerPoints >= 5)
            ShowQuestion(1);
    }

    private void ShowQuestion(int difficulty)
    {
        Debug.Log($"ShowQuestion gọi với difficulty: {difficulty}");
        List<Quest> availableQuestions = questionDatabase.FindAll(q => q.difficulty == difficulty);
        Debug.Log($"Câu hỏi cấp {difficulty}, số lượng: {availableQuestions.Count}");
        if (availableQuestions.Count == 0)
        {
            Debug.LogWarning($"Không có câu hỏi cấp {difficulty}!");
            return;
        }

        currentQuestion = availableQuestions[Random.Range(0, availableQuestions.Count)];
        if (questionUI == null)
        {
            Debug.LogError("questionUI là null!");
            return;
        }
        Debug.Log($"Kích hoạt QuestionUI với câu hỏi: {currentQuestion.questionText}");
        questionUI.gameObject.SetActive(true);
        questionUI.SetQuestion(currentQuestion);
        Debug.Log($"Hiển thị câu hỏi: {currentQuestion.questionText}");
    }

    public void CheckAnswer(string selectedAnswer)
    {
        Debug.Log($"CheckAnswer với: {selectedAnswer}");
        bool isCorrect = selectedAnswer == currentQuestion.correctAnswer;
        if (isCorrect)
        {
            currentPlayerPoints += currentQuestion.pointsReward;
            player.StarText.text = currentPlayerPoints.ToString();
            Debug.Log($"Đúng! +{currentQuestion.pointsReward} điểm");
        }
        else
        {
            player.Hp--;
            player.heartText.text = player.Hp.ToString();
            Debug.Log("Sai! Mất 1 cơ hội");

            if (player.Hp <= 0)
            {
                SceneManager.LoadScene("MenuScene");
                return;
            }

            ShowQuestion(currentQuestion.difficulty);
        }

        questionUI.gameObject.SetActive(false);
    }
}