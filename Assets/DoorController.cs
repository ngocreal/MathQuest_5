using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;

    // Gọi từ enemy khi Bee bị giết
    public void OpenDoorIfBee()
    {
        Debug.Log("Mở cửa do Bee đã bị tiêu diệt.");

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
