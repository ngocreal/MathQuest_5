using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    public int hearts = 4;

    void Awake() { Instance = this; }

    public void AddHeart()
    {
        if (hearts < 4) hearts++;
    }

    public void LoseHeart()
    {
        hearts--;
        if (hearts <= 0)
        {
            Debug.Log("Game Over!");
            GameManager.Instance.GameOver();
        }
    }
}
