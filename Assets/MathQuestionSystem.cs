using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
        if (popUpSystem == null) Debug.LogError("PopUpSystem không được tìm thấy!");
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

    public void ShowQuestion(int difficulty)
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
        // popup trước khi hiển thị UI
        if (popUpSystem != null)
        {
            string popupText = $"Câu hỏi cấp {difficulty}: {currentQuestion.questionText}\nĐáp án: {string.Join(", ", currentQuestion.choices)}";
            popUpSystem.PopUp(popupText);
            Debug.Log($"Popup hiển thị: {popupText}");
        }
        else
        {
            Debug.LogWarning("PopUpSystem null, không hiển thị popup!");
        }
        Debug.Log($"Kích hoạt QuestionUI với câu hỏi: {currentQuestion.questionText}");
        questionUI.gameObject.SetActive(true); // Đảm bảo UI đc kích hoạt
        questionUI.SetQuestion(currentQuestion);
        Debug.Log($"Hiển thị câu hỏi: {currentQuestion.questionText}");

    }

    public void CheckAnswer(string selectedAnswer)
    {
        Debug.Log($"CheckAnswer với: {selectedAnswer}");
        bool isCorrect = selectedAnswer == currentQuestion.correctAnswer;

        if (isCorrect)
        {
            int reward = 2;
            currentPlayerPoints += reward;
            player.StarText.text = currentPlayerPoints.ToString();
            Debug.Log($"Đúng! +{reward} điểm");

            questionUI.gameObject.SetActive(false); //
            if (popUpSystem != null)
            {
                popUpSystem.popUpBox.SetActive(false); //Ẩn po nếu đang hiển thị
            }
        }
        else
        {
            player.Hp--; // Trừ máu
            player.heartText.text = player.Hp.ToString();
            Debug.Log($"Sai! Mất 1 máu, còn lại: {player.Hp}");

            if (player.Hp <= 0)
            {
                Debug.Log("HP = 0, quay về menu");
                SceneManager.LoadScene("MenuScene");
                return;
            }

            questionUI.gameObject.SetActive(false); // Tắt UI cũ trước
            if (popUpSystem != null)
            {
                popUpSystem.popUpBox.SetActive(false); //Tắt po cũ
            }

            ShowQuestion(currentQuestion.difficulty); //Gọi câu hỏi mới
        }
    }


    public string GetSimpleQuestionText()
    {
        // Chọn câu hỏi cấp độ 1 (dễ)
        List<Quest> easyQuestions = questionDatabase.FindAll(q => q.difficulty == 1);
        if (easyQuestions.Count == 0)
        {
            Debug.LogWarning("Không có câu hỏi dễ để hiện popup!");
            return "Không có câu hỏi.";
        }

        Quest popupQuestion = easyQuestions[Random.Range(0, easyQuestions.Count)];
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

        currentQuestion = availableQuestions[Random.Range(0, availableQuestions.Count)];
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
        // Bạn có thể random hoặc cố định item
        string rewardedItemName = "Chocolate Sữa";
        int rewardedQuantity = 1;
        Sprite rewardedSprite = Resources.Load<Sprite>("Sprites/Chocolate Sữa"); // phải khớp tên trong thư mục Resources
        string rewardedDescription = "Restores a small amount of health.";

        // Gọi tới menucontroller để add item
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

}