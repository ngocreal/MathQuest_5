using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public TextMeshProUGUI questionTextUI;
    public Image questionImageUI;
    public GameObject multipleChoice4Panel, multipleChoice2Panel, shortAnswerPanel;
    public Button[] answerButtons4, answerButtons2;
    public TMP_InputField inputFieldAnswer;
    public Button submitButton;

    private Question currentQuestion;
    private string selectedAnswer;
    public AudioClip correctSound, wrongSound;

    void Start()
    {
        // Ẩn QuestionPanel và các panel con khi bắt đầu game
        gameObject.SetActive(false);
        if (multipleChoice4Panel != null) multipleChoice4Panel.SetActive(false);
        if (multipleChoice2Panel != null) multipleChoice2Panel.SetActive(false);
        if (shortAnswerPanel != null) shortAnswerPanel.SetActive(false);
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
        else
        {
            Debug.LogError("Question Text UI chưa được gán trong Inspector!");
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
        else
        {
            Debug.LogError("Question Image UI chưa được gán trong Inspector!");
        }

        // Ẩn tất cả các panel trước khi hiển thị panel phù hợp
        if (multipleChoice4Panel != null) multipleChoice4Panel.SetActive(false);
        if (multipleChoice2Panel != null) multipleChoice2Panel.SetActive(false);
        if (shortAnswerPanel != null) shortAnswerPanel.SetActive(false);

        if (currentQuestion.isMultipleChoice)
        {
            int answerCount = currentQuestion.choices.Length;
            if (answerCount == 4)
            {
                if (multipleChoice4Panel != null)
                {
                    multipleChoice4Panel.SetActive(true);
                    for (int i = 0; i < 4; i++)
                    {
                        answerButtons4[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.choices[i];
                        answerButtons4[i].GetComponent<Image>().color = Color.white;
                        answerButtons4[i].onClick.RemoveAllListeners();
                        int index = i;
                        answerButtons4[i].onClick.AddListener(() => SelectAnswer(index, answerButtons4));
                    }
                }
            }
            else if (answerCount == 2)
            {
                if (multipleChoice2Panel != null)
                {
                    multipleChoice2Panel.SetActive(true);
                    for (int i = 0; i < 2; i++)
                    {
                        answerButtons2[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.choices[i];
                        answerButtons2[i].GetComponent<Image>().color = Color.white;
                        answerButtons2[i].onClick.RemoveAllListeners();
                        int index = i;
                        answerButtons2[i].onClick.AddListener(() => SelectAnswer(index, answerButtons2));
                    }
                }
            }
        }
        else
        {
            if (shortAnswerPanel != null)
            {
                shortAnswerPanel.SetActive(true);
                inputFieldAnswer.text = "";
                submitButton.onClick.RemoveAllListeners();
                submitButton.onClick.AddListener(() => CheckAnswer(inputFieldAnswer.text));
            }
        }

        gameObject.SetActive(true);
    }

    void SelectAnswer(int index, Button[] buttons)
    {
        selectedAnswer = buttons[index].GetComponentInChildren<TextMeshProUGUI>().text;
        foreach (var button in buttons)
        {
            button.GetComponent<Image>().color = Color.white;
        }
        buttons[index].GetComponent<Image>().color = Color.yellow;
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(() => CheckAnswer(selectedAnswer));
    }

    void CheckAnswer(string answer)
    {
        bool isCorrect = answer == currentQuestion.correctAnswer;

        if (currentQuestion.isMultipleChoice)
        {
            Button[] buttons = currentQuestion.choices.Length == 4 ? answerButtons4 : answerButtons2;
            foreach (var button in buttons)
            {
                var buttonText = button.GetComponentInChildren<TextMeshProUGUI>().text;
                if (buttonText == currentQuestion.correctAnswer)
                {
                    button.GetComponent<Image>().color = Color.green;
                }
                else if (buttonText == answer && !isCorrect)
                {
                    button.GetComponent<Image>().color = Color.red;
                }
            }
        }
        else
        {
            inputFieldAnswer.textComponent.color = isCorrect ? Color.green : Color.red;
        }

        AudioSource.PlayClipAtPoint(isCorrect ? correctSound : wrongSound, Camera.main.transform.position);

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