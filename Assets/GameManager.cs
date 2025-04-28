using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public QuestionUI questionUI;
    public XuLyVaCham player;

    private int playerScore = 0;
    private bool isQuestionActive = false;
    private bool hasShownQuestionAt18 = false; // Theo dõi mốc 18 điểm
    private bool hasShownQuestionAt36 = false; // Theo dõi mốc 36 điểm

    private void Awake()
    {
        Debug.Log("GameManager Awake được gọi");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log($"Điểm số khởi tạo: {playerScore}");
    }

    private void Update()
    {
        // Kiểm tra mốc điểm để tự động hiển thị câu hỏi
        if (!isQuestionActive)
        {
            if (!hasShownQuestionAt18 && playerScore >= 18)
            {
                hasShownQuestionAt18 = true;
                DisplayQuestionAtMilestone(2); // Độ khó 2 tại 18 điểm
            }
            else if (!hasShownQuestionAt36 && playerScore >= 36)
            {
                hasShownQuestionAt36 = true;
                DisplayQuestionAtMilestone(3); // Độ khó 3 tại 36 điểm
            }
        }
    }

    public void TriggerQuestion(GameObject triggerObject)
    {
        Debug.Log("TriggerQuestion được gọi với triggerObject: " + (triggerObject != null ? triggerObject.name : "null"));
        if (isQuestionActive)
        {
            Debug.Log("isQuestionActive = true, không hiển thị câu hỏi mới.");
            return;
        }

        // Nếu không đạt mốc điểm, chỉ tiêu diệt quái mà không hiển thị câu hỏi
        if (triggerObject != null && triggerObject.CompareTag("enemy"))
        {
            triggerObject.GetComponent<Enemy_behavior>().Defeat();
            player.DefeatEnemy();
            AddScore(5); // mỗi quái cho 5 điểm
        }
        else if (triggerObject != null && triggerObject.CompareTag("Vang"))
        {
            player.CollectItem();
            AddScore(2); // mỗi vật phẩm cho 2 điểm
        }
    }

    private void DisplayQuestionAtMilestone(int difficulty)
    {
        Debug.Log($"Đạt mốc điểm {playerScore}, hiển thị câu hỏi độ khó {difficulty}");
        isQuestionActive = true;
        player.playerController.canMove = false;

        Question question = QuestionLoader.Instance.GetRandomQuestion(difficulty);
        if (question != null)
        {
            Debug.Log($"Câu hỏi được chọn: {question.questionText}");
            questionUI.DisplayNewQuestion(question);
        }
        else
        {
            Debug.LogError("Không thể tải câu hỏi từ QuestionLoader!");
            isQuestionActive = false;
            player.playerController.canMove = true;
        }
    }

    public void OnAnswerSubmitted(bool isCorrect, int difficulty)
    {
        Debug.Log($"Trả lời câu hỏi: Đúng = {isCorrect}, Độ khó = {difficulty}");
        if (isCorrect)
        {
            AddScore(5); // Tăng điểm khi trả lời đúng
            Debug.Log($"Điểm số mới: {playerScore}");
            questionUI.HideQuestionPanel();
            isQuestionActive = false;
            player.playerController.canMove = true;

            if (player.currentTriggerObject != null)
            {
                if (player.currentTriggerObject.CompareTag("enemy"))
                {
                    player.currentTriggerObject.GetComponent<Enemy_behavior>().Defeat();
                    player.DefeatEnemy();
                }
                else if (player.currentTriggerObject.CompareTag("Vang"))
                {
                    player.CollectItem();
                }
            }
        }
        else
        {
            questionUI.HideQuestionPanel();
            isQuestionActive = false;
            player.playerController.canMove = true;

            if (player.currentTriggerObject != null)
            {
                if (player.currentTriggerObject.CompareTag("enemy"))
                {
                    player.TakeDamage();
                    if (player.Hp > 0)
                    {
                        TriggerQuestion(null);
                    }
                }
                else if (player.currentTriggerObject.CompareTag("Vang"))
                {
                    player.SkipItem();
                }
            }
        }
    }

    private int GetDifficultyFromScore(int score)
    {
        Debug.Log($"Tính độ khó với điểm số: {score}");
        if (score < 20) return 1;
        if (score < 50) return 2;
        return 3;
    }

    public void AddScore(int points)
    {
        playerScore += points;
        Debug.Log($"Điểm số mới: {playerScore}");
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }
}