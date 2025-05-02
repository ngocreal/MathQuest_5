using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questionText;
    public string imagePath;
    public string correctAnswer;
    public bool isMultipleChoice;
    public string[] choices;
    public int difficulty;
}

