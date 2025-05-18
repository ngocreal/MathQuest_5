using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Image questionImage;
    public Button[] choiceButtons;
    private MathQuestionSystem questionSystem;

    private void Awake()
    {
        questionSystem = FindFirstObjectByType<MathQuestionSystem>();
        Debug.Log("QuestUI Awake, questionSystem: " + (questionSystem != null ? "found" : "null") + ", gameObject active: " + gameObject.activeSelf);
    }

    private void Start()
    {
        Debug.Log("QuestUI Start, panel active: " + gameObject.activeSelf);
    }

    public void SetQuestion(Quest question)
    {
        Debug.Log($"SetQuestion gọi với: {question.questionText}, panel active: {gameObject.activeSelf}");
        if (questionText == null) Debug.LogError("questionText is null!");
        else
        {
            questionText.text = question.questionText;
            Debug.Log("questionText set to: " + question.questionText + ", text active: " + questionText.gameObject.activeSelf);
        }

        questionImage.gameObject.SetActive(!string.IsNullOrEmpty(question.imagePath));
        if (!string.IsNullOrEmpty(question.imagePath))
        {
            Sprite img = Resources.Load<Sprite>(question.imagePath);
            if (img) questionImage.sprite = img;
            else Debug.LogWarning("Image not found: " + question.imagePath);
        }

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < question.choices.Length)
            {
                if (choiceButtons[i] == null) Debug.LogError($"choiceButtons[{i}] is null!");
                else
                {
                    choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.choices[i];
                    int index = i;
                    choiceButtons[i].onClick.RemoveAllListeners();
                    choiceButtons[i].onClick.AddListener(() => questionSystem.CheckAnswer(question.choices[index]));
                    choiceButtons[i].gameObject.SetActive(true);
                    Debug.Log($"Button {i} set to: {question.choices[i]}, button active: {choiceButtons[i].gameObject.activeSelf}");
                }
            }
            else
            {
                if (choiceButtons[i] != null) choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }
}