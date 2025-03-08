using UnityEngine;

public class XuLyVaCham : MonoBehaviour
{
    public int Hp = 3;
    public int Star = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Vang"))
        { 
            Star++;
            Destroy(colision.gameObject);
        }
        if(colision.CompareTag("GaiNhon"))
        {
            Hp--;
        }
 
        
    }
}
