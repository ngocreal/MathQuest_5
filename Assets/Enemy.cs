using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    public Transform leftPoint, rightPoint; 
    private bool movingRight = true;

    public int health = 4;

    void Start()
    {
        transform.position = leftPoint.position;
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, rightPoint.position) < 0.1f)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, leftPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, leftPoint.position) < 0.1f)
            {
                movingRight = true;
                Flip();
            }
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage TAKEN!");
    }
}
