using System.Collections.Generic;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    public List<Question> easyQuestions = new List<Question>();
    public List<Question> mediumQuestions = new List<Question>();
    public List<Question> hardQuestions = new List<Question>();

    void Start()
    {
        LoadQuestions();
    }

    void LoadQuestions()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("questions"); // Tên file JSON
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            Question[] allQuestions = JsonUtility.FromJson<QuestionArray>("{\"questions\":" + jsonText + "}").questions;

            foreach (Question q in allQuestions)
            {
                if (q.difficulty == 1) easyQuestions.Add(q);
                else if (q.difficulty == 2) mediumQuestions.Add(q);
                else if (q.difficulty == 3) hardQuestions.Add(q);
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy file JSON!");
        }
    }
}

[System.Serializable]
public class QuestionArray
{
    public Question[] questions;
}
