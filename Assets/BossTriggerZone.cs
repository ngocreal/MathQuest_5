using UnityEngine;

public class BossTriggerZone : MonoBehaviour
{
    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Player nh?n Q g?n Boss, g?i h? th?ng câu h?i...");
            //GameManager.Instance.TriggerBossQuestion(gameObject); // G?i t? GameManager
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
