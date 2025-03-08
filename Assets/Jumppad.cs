using UnityEngine;

public class Jumppad : MonoBehaviour
{
    public float doNay = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*doNay,ForceMode2D.Impulse);
        }
    }
}
