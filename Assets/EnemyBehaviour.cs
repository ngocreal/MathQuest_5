using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float Hitpoints;
    public float MaxHitpoints = 4;

    void Start()
    {
        Hitpoints = MaxHitpoints;
    }

   public void TakeHit(float damage)
    {
        Destroy(gameObject);
    }
}
