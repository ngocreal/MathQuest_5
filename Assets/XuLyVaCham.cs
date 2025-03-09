using UnityEngine;
using TMPro;
using System.Collections;

public class XuLyVaCham : MonoBehaviour
{
    public int Hp = 3;
    public int Star = 0;
    public TextMeshProUGUI heartText;
    public TextMeshProUGUI StarText;
    public Animator animator;  // Thêm Animator để điều khiển animation

    private Vector2 spawnPoint; // Lưu vị trí ban đầu của nhân vật
    private bool isDead = false; // Để tránh chết nhiều lần khi đang trong quá trình respawn

    void Start()
    {
        spawnPoint = transform.position; // Lưu vị trí nhân vật khi vào level
        StarText.SetText(Star.ToString());
        heartText.SetText(Hp.ToString());
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Vang"))
        {
            Star++;
            StarText.SetText(Star.ToString());
            Destroy(colision.gameObject);
        }
        if (colision.CompareTag("GaiNhon"))
        {
            Hp--;
            heartText.SetText(Hp.ToString());

            if (Hp <= 0 && !isDead)
            {
                StartCoroutine(DieAndRespawn());
            }
        }
    }

    IEnumerator DieAndRespawn()
    {
        isDead = true; 
        animator.SetTrigger("Death"); 

        yield return new WaitForSeconds(1.5f); 

        transform.position = spawnPoint; 
        Hp = 3; 
        heartText.SetText(Hp.ToString()); 
        isDead = false; 
    }
}
