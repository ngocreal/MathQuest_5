using UnityEngine;

public class Dc : MonoBehaviour
{ 
    public Rigidbody2D rb;

    public Animator anim;

    public int tocDo = 4;

    public float lucNhay = 5f;
    private bool duocPhepNhay;
    private bool nhayDoi;

    public Transform _duocPhepNhay;
    public LayerMask san;

    public float traiPhai;
    public bool isfacingRight = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        duocPhepNhay = Physics2D.OverlapCircle(_duocPhepNhay.position,0.2f,san);

        traiPhai = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(tocDo * traiPhai, rb.linearVelocityY);

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
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("TanCong");
        }

        if (duocPhepNhay)
        {
            nhayDoi = true; 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (duocPhepNhay)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, lucNhay);
                duocPhepNhay = false; 
            }
            else if (nhayDoi)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, lucNhay);
                nhayDoi = false;
            }
        }

    }
}

