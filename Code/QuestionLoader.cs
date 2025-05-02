using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionList
    {
        public List<Question> questions;

    }

    public class QuestionLoader : MonoBehaviour
    {
        public static QuestionLoader Instance;
        private List<Question> questions;

        private void Awake()
        {
            Debug.Log("QuestionLoader Awake được gọi");
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            LoadQuestions();
        }

        private void LoadQuestions()
        {
            Debug.Log("Bắt đầu tải questions.json");
            TextAsset jsonText = Resources.Load<TextAsset>("questions");
            if (jsonText != null)
            {
                Debug.Log("Đã tìm thấy file questions.json, nội dung: " + jsonText.text);
                try
                {
                    QuestionList questionList = JsonUtility.FromJson<QuestionList>(jsonText.text);
                    if (questionList != null && questionList.questions != null)
                    {
                        questions = questionList.questions;
                        Debug.Log($"Đã tải {questions.Count} câu hỏi từ questions.json");
                        if (questions.Count > 0)
                        {
                            Debug.Log("Danh sách câu hỏi:");
                            foreach (var q in questions)
                            {
                                Debug.Log($"Câu hỏi: {q.questionText}, Đáp án đúng: {q.correctAnswer}, Độ khó: {q.difficulty}");
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("QuestionList rỗng hoặc không hợp lệ!");
                        questions = new List<Question>();
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Lỗi khi parse JSON: " + e.Message);
                    questions = new List<Question>();
                }
            }
            else
            {
                Debug.LogError("Không tìm thấy file questions.json trong Resources!");
                questions = new List<Question>();
            }
        }

        public Question GetRandomQuestion(int difficulty)
        {
            Debug.Log($"Tìm câu hỏi với độ khó: {difficulty}");
            List<Question> filteredQuestions = questions.FindAll(q => q.difficulty == difficulty);
            if (filteredQuestions.Count == 0)
            {
                Debug.LogWarning($"Không có câu hỏi nào ở độ khó {difficulty}, thử lấy câu hỏi từ độ khó khác...");
                filteredQuestions = new List<Question>(questions);
            }

            if (filteredQuestions.Count > 0)
            {
                Question selectedQuestion = filteredQuestions[Random.Range(0, filteredQuestions.Count)];
                Debug.Log($"Đã chọn câu hỏi ngẫu nhiên: {selectedQuestion.questionText}");
                return selectedQuestion;
            }

            Debug.LogError("Không có câu hỏi nào để chọn!");
            return null;
        }
    }

