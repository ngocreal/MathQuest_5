using UnityEngine;

public class AnswerChecker : MonoBehaviour
{
    public static AnswerChecker Instance;

    void Awake() { Instance = this; }

    public void Check(int playerAnswer, int correctAnswer)
    {
        if (playerAnswer == correctAnswer)
        {
            PlayerHealth.Instance.AddHeart(); // Trả lời đúng được +1 tim (nếu chưa đạt tối đa)
            Debug.Log("+1 tim");
        }
        else
        {
            PlayerHealth.Instance.LoseHeart(); // Trừ tim nếu trả lời sai
            Debug.Log("-1 tim");
        }
    }
}

