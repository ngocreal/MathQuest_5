using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.Splines;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    public UnityEngine.Transform leftPoint, rightPoint;
    private bool movingRight = true;
    // Start is called before the first frame update 
    void Start()
    {
        transform.position = leftPoint.position;
    }

    // Update is called once per frame 
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, rightPoint.position) < 0.1f)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, leftPoint.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, leftPoint.position) < 0.1f)
            {
                movingRight = true;
            }
        }
    }
}
