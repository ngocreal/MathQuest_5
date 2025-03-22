using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Image questionImage;
    public GameObject multipleChoice4Panel, multipleChoice2Panel, shortAnswerPanel;
    public Button[] answerButtons4, answerButtons2;
    public TMP_InputField inputFieldAnswer;
    public Button submitButton, closeButton;

    private Question currentQuestion;

    void Start()
    {
        closeButton.onClick.AddListener(HideQuestionPanel);
    }

    public void DisplayNewQuestion(Question newQuestion)
    {
        currentQuestion = newQuestion;

        // Hiển thị câu hỏi
        questionText.text = currentQuestion.questionText;
        questionText.gameObject.SetActive(true);

        // Hiển thị hình ảnh (nếu có)
        if (!string.IsNullOrEmpty(currentQuestion.imagePath))
        {
            questionImage.sprite = LoadImage(currentQuestion.imagePath);
            questionImage.gameObject.SetActive(true);
        }
        else
        {
            questionImage.gameObject.SetActive(false);
        }

        // Ẩn toàn bộ UI trước khi hiển thị dạng câu hỏi đúng
        multipleChoice4Panel.SetActive(false);
        multipleChoice2Panel.SetActive(false);
        shortAnswerPanel.SetActive(false);

        // Xử lý câu hỏi trắc nghiệm
        if (currentQuestion.isMultipleChoice)
        {
            int answerCount = currentQuestion.choices.Length;
            if (answerCount == 4)
            {
                multipleChoice4Panel.SetActive(true);
                for (int i = 0; i < 4; i++)
                {
                    answerButtons4[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.choices[i];
                    answerButtons4[i].onClick.RemoveAllListeners();
                    answerButtons4[i].onClick.AddListener(() => CheckAnswer(currentQuestion.choices[i]));
                }
            }
            else if (answerCount == 2)
            {
                multipleChoice2Panel.SetActive(true);
                for (int i = 0; i < 2; i++)
                {
                    answerButtons2[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.choices[i];
                    answerButtons2[i].onClick.RemoveAllListeners();
                    answerButtons2[i].onClick.AddListener(() => CheckAnswer(currentQuestion.choices[i]));
                }
            }
        }
        else // Câu hỏi tự luận
        {
            shortAnswerPanel.SetActive(true);
            inputFieldAnswer.text = "";
            submitButton.onClick.RemoveAllListeners();
            submitButton.onClick.AddListener(() => CheckAnswer(inputFieldAnswer.text));
        }

        gameObject.SetActive(true);
    }

    void CheckAnswer(string answer)
    {
        if (answer == currentQuestion.correctAnswer)
        {
            Debug.Log("Đáp án đúng! Tiếp tục chơi.");
            HideQuestionPanel();
        }
        else
        {
            Debug.Log("Sai rồi! Trừ tim.");
        }
    }

    void HideQuestionPanel()
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
