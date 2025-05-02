using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public TextMeshProUGUI questionTextUI;
    public Image questionImageUI;
    public GameObject multipleChoice4Panel;
    public Button[] answerButtons4;
    public AudioClip correctSound, wrongSound;

    private Question currentQuestion;
    private string selectedAnswer;

    void Start()
    {
        gameObject.SetActive(false);
        if (multipleChoice4Panel != null) multipleChoice4Panel.SetActive(false);
    }

    public void DisplayNewQuestion(Question newQuestion)
    {
        if (newQuestion == null)
        {
            Debug.LogError("Không có câu hỏi để hiển thị!");
            return;
        }

        currentQuestion = newQuestion;
        selectedAnswer = null;

        // Hiển thị câu hỏi
        if (questionTextUI != null)
        {
            questionTextUI.text = currentQuestion.questionText;
            questionTextUI.gameObject.SetActive(true);
        }

        // Hiển thị hình ảnh (nếu có)
        if (questionImageUI != null)
        {
            if (!string.IsNullOrEmpty(currentQuestion.imagePath))
            {
                questionImageUI.sprite = LoadImage(currentQuestion.imagePath);
                questionImageUI.gameObject.SetActive(true);
            }
            else
            {
                questionImageUI.gameObject.SetActive(false);
            }
        }

        // Hiển thị panel câu hỏi 4 đáp án
        if (multipleChoice4Panel != null)
        {
            multipleChoice4Panel.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                answerButtons4[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.choices[i];
                answerButtons4[i].GetComponent<Image>().color = Color.white;
                answerButtons4[i].onClick.RemoveAllListeners();
                int index = i;
                answerButtons4[i].onClick.AddListener(() => SelectAnswer(index));
            }
        }

        gameObject.SetActive(true);
    }

    void SelectAnswer(int index)
    {
        selectedAnswer = currentQuestion.choices[index];

        // Reset màu
        foreach (var button in answerButtons4)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        // Tô màu vàng cho lựa chọn
        answerButtons4[index].GetComponent<Image>().color = Color.yellow;

        // Kiểm tra đáp án ngay lập tức
        CheckAnswer(selectedAnswer);
    }

    void CheckAnswer(string answer)
    {
        bool isCorrect = answer == currentQuestion.correctAnswer;

        // Tô màu đúng/sai
        for (int i = 0; i < 4; i++)
        {
            string btnText = answerButtons4[i].GetComponentInChildren<TextMeshProUGUI>().text;
            if (btnText == currentQuestion.correctAnswer)
            {
                answerButtons4[i].GetComponent<Image>().color = Color.green;
            }
            else if (btnText == answer && !isCorrect)
            {
                answerButtons4[i].GetComponent<Image>().color = Color.red;
            }
        }

        // Phát âm thanh đúng/sai
        AudioSource.PlayClipAtPoint(isCorrect ? correctSound : wrongSound, Camera.main.transform.position);

        // Báo về GameManager (ví dụ để xử lý tiếp)
        GameManager.Instance.OnAnswerSubmitted(isCorrect, currentQuestion.difficulty);
    }

    public void HideQuestionPanel()
    {
        gameObject.SetActive(false);
    }

    Sprite LoadImage(string path)
    {
        Texture2D texture = Resources.Load<Texture2D>(path);
        if (texture != null)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        return null;
    }
}