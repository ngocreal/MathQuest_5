using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public QuestDatabase questionDatabase;
    public MathQuestionSystem questionSystem;

    private bool hasWarnedMissingSystem = false;

    private void Awake()
    {
        if (Instance == null)
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
        else if (Instance != this)
        {
            Debug.LogWarning("GameManager khác đã tồn tại, tiêu diệt bản sao.");
            Destroy(gameObject);
            return;
        }

        // Tự động gán questionSystem nếu chưa có
        AssignQuestionSystem();
    }

    private void Update()
    {
        // Thử gán lại questionSystem nếu chưa có
        if (questionSystem == null && !hasWarnedMissingSystem)
        {
            AssignQuestionSystem();
        }
    }

    private void AssignQuestionSystem()
    {
        if (questionSystem == null)
        {
            questionSystem = FindFirstObjectByType<MathQuestionSystem>();
            if (questionSystem == null)
            {
                Debug.LogWarning("Không tìm thấy MathQuestionSystem trong scene! Sẽ thử lại trong Update.");
                hasWarnedMissingSystem = true;
            }
            else
            {
                Debug.Log("Đã tự động gán MathQuestionSystem cho GameManager.");
                hasWarnedMissingSystem = false;
            }
        }
    }

    public void TriggerQuestion(GameObject triggerObject)
    {
        Debug.Log($"TriggerQuestion gọi cho: {triggerObject.tag}");

        // Kiểm tra null
        if (questionSystem == null)
        {
            Debug.LogError("questionSystem chưa được gán! Không thể cộng điểm.");
            return;
        }

        if (triggerObject.CompareTag("enemy"))
        {
            Debug.Log("Giết quái +4 điểm");
            questionSystem.AddPoints(4);
        }
        else if (triggerObject.CompareTag("Vang"))
        {
            Debug.Log("+1 điểm cho Vang");
            questionSystem.AddPoints(1);
        }
    }
}