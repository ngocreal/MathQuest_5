using UnityEngine;

public class Chest : MonoBehaviour
{
    public PopUpSystem popUpSystem;
    private bool isPlayerNearby = false;
    private MathQuestionSystem mathQuestionSystem;

    void Start()
    {
        mathQuestionSystem = FindFirstObjectByType<MathQuestionSystem>();
        if (mathQuestionSystem == null)
            Debug.LogError("MathQuestionSystem not found!");
        Time.timeScale = 1;
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed near chest");

            Quest selectedQuest = mathQuestionSystem.ShowQuestionAndReturn(1); // Lấy câu hỏi cấp 1
            if (selectedQuest != null)
            {
                string popupText = $"Câu hỏi: {selectedQuest.questionText}\n(Chọn đáp án đúng bên dưới)";
                popUpSystem.PopUp(popupText); // Chỉ để minh họa câu hỏi, trả lời bằng UI
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
