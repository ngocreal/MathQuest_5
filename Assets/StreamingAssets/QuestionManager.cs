using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Question
{
    public int id;
    public string question;
    public int answer;
    public string difficulty;
}

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;
    private List<Question> questions;

    void Awake()
    {
        if (Instance == null) Instance = this;
        LoadQuestions();
    }

    void LoadQuestions()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("questions");
        questions = JsonUtility.FromJson<QuestionList>("{\"questions\":" + jsonFile.text + "}").questions;
    }

    public Question GetRandomQuestion()
    {
        return questions[Random.Range(0, questions.Count)];
    }
}

[System.Serializable]
public class QuestionList
{
    public List<Question> questions;
}
