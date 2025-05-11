using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI3 : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI questionText;
    public Image questionImage;
    public Button[] choiceButtons;

    [System.Serializable]
    public class QuestionData
    {
        public string questionText;
        public string imagePath;
        public string correctAnswer;
        public bool isMultipleChoice;
        public string[] choices;
    }

    private QuestionData question = new QuestionData
    {
        questionText = "Câu hỏi 3: So sánh hai số: 987 .?. 1 082",
        imagePath = "",
        correctAnswer = "<",
        isMultipleChoice = true,
        choices = new string[]
        {
            "A.<",
            "B.>",
            "C.=",
            "D.#"
        }
    };

    void Start()
    {
        gameObject.SetActive(true);
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        questionText.text = question.questionText;

        if (!string.IsNullOrEmpty(question.imagePath))
        {
            // Load image from Resources folder (if needed)
            Sprite img = Resources.Load<Sprite>(question.imagePath);
            if (img != null)
            {
                questionImage.sprite = img;
                questionImage.gameObject.SetActive(true);
            }
        }
        else
        {
            questionImage.gameObject.SetActive(false);
        }

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < question.choices.Length)
            {
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.choices[i];
                int index = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => CheckAnswer(index));
                choiceButtons[i].gameObject.SetActive(true);
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void CheckAnswer(int index)
    {
        string selected = question.choices[index];
        if (selected == question.correctAnswer)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect!");
        }
    }
}

