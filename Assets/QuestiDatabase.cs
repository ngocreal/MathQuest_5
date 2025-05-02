using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionList
{
    public List<Quest> questions;
}

public class QuestDatabase : MonoBehaviour
{
    public TextAsset questionJson;

    public List<Quest> GetQuestions()
    {
        QuestionList questionList = JsonUtility.FromJson<QuestionList>(questionJson.text);
        return questionList.questions;
    }

    public Quest GetRandomQuestionByDifficulty(int difficulty)
    {
        var questions = GetQuestions().FindAll(q => q.difficulty == difficulty);
        return questions[Random.Range(0, questions.Count)];
    }
}

