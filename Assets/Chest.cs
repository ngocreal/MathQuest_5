using UnityEngine;

public class Chest : MonoBehaviour
{
    public PopUpSystem popUpSystem;
    public string message = "Câu hỏi 1: Số 57 308. Viết là?";
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed near chest");
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

