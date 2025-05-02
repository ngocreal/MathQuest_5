using System.Collections.Generic;
using UnityEngine;


public class QuestionDatabase : MonoBehaviour
{
    public List<Question> questions = new List<Question>();

    public Question GetRandomQuestionByDifficulty(int difficulty)
    {
        List<Question> filtered = questions.FindAll(q => q.difficulty == difficulty);
        if (filtered.Count == 0) return null;
        return filtered[Random.Range(0, filtered.Count)];
    }
}