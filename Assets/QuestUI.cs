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
        Canvas canvas = GetComponentInParent<Canvas>();
        Debug.Log("QuestUI Awake, gameObject active: " + gameObject.activeSelf + ", Canvas active: " + (canvas != null ? canvas.gameObject.activeSelf : "no canvas") + ", Canvas scaleFactor: " + (canvas != null ? canvas.scaleFactor : 0));
    }

    public void SetQuestion(Quest question)
    {
        Debug.Log("SetQuestion called, panel active: " + gameObject.activeSelf);
        if (questionText == null) Debug.LogError("questionText is null!");
        else questionText.text = question.questionText;

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
                if (choiceButtons[i] == null) Debug.LogError("choiceButtons[" + i + "] is null!");
                else
                {
                    choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.choices[i];
                    int index = i;
                    choiceButtons[i].onClick.RemoveAllListeners();
                    choiceButtons[i].onClick.AddListener(() => questionSystem.CheckAnswer(question.choices[index]));
                    choiceButtons[i].gameObject.SetActive(true);
                }
            }
            else
            {
                if (choiceButtons[i] != null) choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }
}