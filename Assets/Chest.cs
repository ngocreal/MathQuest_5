using UnityEngine;

public class Chest : MonoBehaviour
{
    public PopUpSystem popUpSystem;
    public string message = "Bạn đã tìm thấy một rương bí ẩn!";
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            popUpSystem.PopUp(message);
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
