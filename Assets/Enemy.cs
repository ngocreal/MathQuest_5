using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    public Transform leftPoint, rightPoint; // Điểm đánh dấu giới hạn
    private bool movingRight = true;

    void Start()
    {
        // Đảm bảo enemy bắt đầu ở vị trí chính giữa hai điểm đánh dấu
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
    }

    void Flip()
    {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
