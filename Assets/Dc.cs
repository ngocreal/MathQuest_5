using UnityEngine;

public class Dc : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    public int tocDo = 4;
    public float lucNhay = 5f;
    private bool duocPhepNhay;
    private bool nhayDoi;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public Transform _duocPhepNhay;
    public LayerMask san;

    public float traiPhai;
    public bool isfacingRight = true;
    public ParticleSystem SmokeFX;

    public bool canMove = true; 


    void Start()
    {
        respawnPoint = transform.position;
    }

    void Update()
    {
        if (!canMove) return; 

        duocPhepNhay = Physics2D.OverlapCircle(_duocPhepNhay.position, 0.2f, san);

        traiPhai = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(tocDo * traiPhai, rb.linearVelocity.y);

        if (isfacingRight == true && traiPhai == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isfacingRight = false;

        }
        if (isfacingRight == false && traiPhai == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isfacingRight = true;
        }

        anim.SetFloat("dichuyen", Mathf.Abs(traiPhai));

        if (Input.GetMouseButtonDown(0) && canMove)
        {
            anim.SetTrigger("TanCong");
        }

        if (duocPhepNhay)
        {
            nhayDoi = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            if (duocPhepNhay)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, lucNhay);
                duocPhepNhay = false;
                SmokeFX.Play();
            }
            else if (nhayDoi)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, lucNhay);
                nhayDoi = false;
                SmokeFX.Play();
            }
        }

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
    }
}
