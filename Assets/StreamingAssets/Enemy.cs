using UnityEngine;

public class Enemy
{
    public class Enemy : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.PauseGame(); // Dừng game
                Question randomQuestion = QuestionManager.Instance.GetRandomQuestion(GameManager.Instance.GetDifficultyLevel());
                UIManager.Instance.ShowQuestionPanel(randomQuestion);
            }
        }
    }


}
