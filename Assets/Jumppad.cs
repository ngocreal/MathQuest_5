using UnityEngine;

public class Jumppad : MonoBehaviour
{
    public float doNay = 10f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); // Lấy Animator của Jumppad
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * doNay, ForceMode2D.Impulse);
            anim.SetTrigger("JumpPad");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.ResetTrigger("JumpPad"); 
        }
    }
}
