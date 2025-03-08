using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FallTrap : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool daRoi = false;
    public Transform diemKhoiPhuc;
    // Start is called before the first frame update 
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !daRoi)
        {
            rb.isKinematic = false;
            daRoi = true;
            Invoke("KhoiPhuc", 2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("MathQuest_5");
        }
    }

    private void KhoiPhuc()
    {
        rb.isKinematic = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularDamping = 0;
        transform.position = diemKhoiPhuc.position;
        daRoi = false;
    }
}
