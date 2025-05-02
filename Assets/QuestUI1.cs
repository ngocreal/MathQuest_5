using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;


public class QuestUI1 : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string imagePath;
        public string correctAnswer;
        public bool isMultipleChoice;
        public List<string> choices;
    }

    public List<Question> questionList;
    public TMP_Text questionText;
    public Image questionImage;
    public List<Button> choiceButtons;
    public GameObject popUpBox;
    public Animator animator;

    private Question currentQuestion;

    void Start()
    {
        popUpBox.SetActive(false);
        LoadQuestionsFromJSON();
    }

    public void ShowRandomQuestion()
    {
        currentQuestion = questionList[Random.Range(0, questionList.Count)];
        popUpBox.SetActive(true);
        animator.SetTrigger("PopUp");

        questionText.text = currentQuestion.questionText;

        // Load image if path exists (optional)
        if (!string.IsNullOrEmpty(currentQuestion.imagePath))
        {
            Sprite sprite = Resources.Load<Sprite>(currentQuestion.imagePath);
            if (sprite != null)
            {
                questionImage.gameObject.SetActive(true);
                questionImage.sprite = sprite;
            }
            else
            {
                questionImage.gameObject.SetActive(false);
            }
        }
        else
        {
            questionImage.gameObject.SetActive(false);
        }

        // Assign choices
        for (int i = 0; i < choiceButtons.Count; i++)
        {
            if (i < currentQuestion.choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                TMP_Text btnText = choiceButtons[i].GetComponentInChildren<TMP_Text>();
                btnText.text = currentQuestion.choices[i];

                string choice = currentQuestion.choices[i];
                choiceButtons[i].onClick.RemoveAllListeners(); // Clear old listeners
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choice));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnChoiceSelected(string selectedAnswer)
    {
        if (selectedAnswer == currentQuestion.correctAnswer)
        {
            Debug.Log("Đúng!");
            animator.SetTrigger("PopUpClose"); // Trigger đóng popup
        }
        else
        {
            Debug.Log("Sai rồi!");
        }
    }

    [System.Serializable]
    public class QuestionListWrapper
    {
        public List<Question> questions;
    }

    void LoadQuestionsFromJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Question1"); // không cần .json
        if (jsonFile != null)
        {
            questionList = JsonUtilityWrapper.FromJsonArray<Question>(jsonFile.text);
        }
        else
        {
            Debug.LogError("Không tìm thấy file Question.json trong Resources!");
        }
    }

    public static class JsonUtilityWrapper
    {
        public static List<T> FromJsonArray<T>(string json)
        {
            string newJson = "{\"array\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public List<T> array;
        }
    }

}
