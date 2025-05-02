using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] choices;
    public string correctAnswer;
    public string imagePath;
    public int difficulty;
    public bool isMultipleChoice;
}

[System.Serializable]
public class QuestionListWrapper
{
    public List<Question> questions;
}


