using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public QuestDatabase questionDatabase;
    public MathQuestionSystem questionSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            if (transform.parent == null)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning("GameManager không phải root GameObject. Đã tách ra để DontDestroyOnLoad.");
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
        }

        // tự động gán questionSystem nếu chưa có
        if (questionSystem == null)
        {
            questionSystem = FindFirstObjectByType<MathQuestionSystem>();
            if (questionSystem == null)
            {
                Debug.LogError("Không tìm thấy MathQuestionSystem trong scene!");
            }
            else
            {
                Debug.Log("Đã tự động gán MathQuestionSystem cho GameManager.");
            }
        }
    }

    public void TriggerQuestion(GameObject triggerObject)
    {
        Debug.Log($"TriggerQuestion gọi cho: {triggerObject.tag}");

        // kiểm tra null
        if (questionSystem == null)
        {
            Debug.LogError("questionSystem chưa được gán! Không thể cộng điểm.");
            return;
        }

        int difficulty = 1;
        if (triggerObject.CompareTag("enemy"))
        {
            difficulty = 2;
            Debug.Log("Cộng 4 điểm cho enemy");
            questionSystem.AddPoints(4);
        }
        else if (triggerObject.CompareTag("Vang"))
        {
            difficulty = 1;
            Debug.Log("Cộng 1 điểm cho Vang");
            questionSystem.AddPoints(1);
        }
    }
}
