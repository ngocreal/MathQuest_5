using UnityEngine;

public class Climb : MonoBehaviour
{
    public float climbSpeed = 3f;
    private bool isClimbing = false;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero; 
            anim.SetBool("isClimbing", true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = 2f;
            anim.SetBool("isClimbing", false); 
            anim.SetTrigger("Idle"); 
        }
    }

    private void Update() 
    {
        if (isClimbing)
        {
            float climbInput = Input.GetAxisRaw("Vertical");

            if (climbInput != 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, climbInput * climbSpeed);
                anim.SetBool("isClimbing", true); 
            }
            else
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, 0);
                anim.SetBool("isClimbing", false); 
            }
        }
    }
}
