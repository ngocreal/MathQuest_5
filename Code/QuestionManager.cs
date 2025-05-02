using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public GameObject questionUI;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    private string correctAnswer;

    void Start()
    {
        questionUI.SetActive(false);
    }

    public void ShowRandomQuestion()
    {
        questionUI.SetActive(true);
        GenerateQuestion(DifficultySettings.SelectedDifficulty);
    }

    void GenerateQuestion(Difficulty difficulty)
    {
        string question = "";
        string[] options = new string[3];
        correctAnswer = "";

        switch (difficulty)
        {
            case Difficulty.Easy:
                int a = Random.Range(1, 10);
                int b = Random.Range(1, 10);
                int result = a + b;
                question = $"What is {a} + {b}?";
                correctAnswer = result.ToString();
                options[0] = result.ToString();
                options[1] = (result + 1).ToString();
                options[2] = (result - 1).ToString();
                break;

            case Difficulty.Medium:
                int x = Random.Range(2, 10);
                int y = Random.Range(2, 10);
                int product = x * y;
                question = $"What is {x} x {y}?";
                correctAnswer = product.ToString();
                options[0] = product.ToString();
                options[1] = (product + 5).ToString();
                options[2] = (product - 3).ToString();
                break;

            case Difficulty.Hard:
                int p = Random.Range(2, 20);
                int q = Random.Range(1, p);
                int diff = p - q;
                question = $"What is {p} - {q}?";
                correctAnswer = diff.ToString();
                options[0] = diff.ToString();
                options[1] = (diff + 2).ToString();
                options[2] = (diff - 1).ToString();
                break;
        }

        questionText.text = question;
        options = ShuffleArray(options); // ??o ng?u nhiên

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i];
            answerButtons[i].onClick.RemoveAllListeners();
            string chosenAnswer = options[i];
            answerButtons[i].onClick.AddListener(() => CheckAnswer(chosenAnswer));
        }
    }

    string[] ShuffleArray(string[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rand = Random.Range(i, array.Length);
            string temp = array[i];
            array[i] = array[rand];
            array[rand] = temp;
        }
        return array;
    }

    void CheckAnswer(string answer)
    {
        if (answer == correctAnswer)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong!");
        }
        questionUI.SetActive(false);
    }
}

