using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Dừng xóa hay chỉnh sửa code trong XuLyVaCham.cs và cái Enemy.cs lần trước bà sửa sao cái nó chạy éo được luôn sửa lại thì cũng éo chạy nếu bà muốn thêm cái gì
    //thì tạo Cs khác rồi chat hỏi kết nối mà không làm thay đổi cái cs này.

    [SerializeField] private float speed = 1f;
    public Transform leftPoint, rightPoint;
    private bool movingRight = true;

    public int health = 3; // Thêm thuộc tính máu

    void Start()
    {
        transform.position = leftPoint.position;
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
    }
}
