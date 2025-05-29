using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    public Transform leftPoint, rightPoint;
    private bool movingRight = true;

    public DoorController doorController;
    public string enemyName = "Bee";



    public int health = 3; // Thêm thuộc tính máu

    void Start()
    {
        transform.position = leftPoint.position;
        Debug.Log("Enemy " + gameObject.name + " is alive with HP: " + health);
    }

    void Update()
    {
        Move();

        if (health <= 0)
        {
            Destroy(gameObject); // Enemy biến mất khi máu <= 0
        }
    }

    void Move()
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
        Debug.Log("Enemy took " + damage + " damage. Current health: " + health);

        if (health <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Debug.Log(enemyName + " đã chết.");

        if (enemyName == "Bee" && doorController != null)
        {
            doorController.OpenDoorIfBee();
        }

        Destroy(gameObject);
    }

}
