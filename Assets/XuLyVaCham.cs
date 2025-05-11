using System.Collections;
using TMPro;
using UnityEngine;

public class XuLyVaCham : MonoBehaviour
{
    public int Hp = 2;
    public int maxHealth = 3;
    public int Star = 0;
    public TextMeshProUGUI heartText;
    public TextMeshProUGUI StarText;
    public Animator animator;
    public Dc playerController;
    private Vector2 spawnPoint;
    private bool isDead = false;

    void Start()
    {
        spawnPoint = transform.position;
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
        else if (colision.CompareTag("GaiNhon") || colision.CompareTag("enemy"))
        {
            Hp--;
            heartText.SetText(Hp.ToString());

            if (Hp > 0)
            {
                animator.SetTrigger("Hurt");
            }

            if (Hp <= 0 && !isDead)
            {
                StartCoroutine(DieAndRespawn());
            }
        }
    }

    public void RestoreHealth(int amount)
    {
        Debug.Log("Change Health called.");
        Hp += amount;
        if (Hp > maxHealth)
        {
            Hp = maxHealth;
        }
        heartText.SetText(Hp.ToString());
    }

    IEnumerator DieAndRespawn()
    {
        isDead = true;
        playerController.canMove = false;
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(1.5f);

        transform.position = spawnPoint;
        Hp = maxHealth;
        heartText.SetText(Hp.ToString());

        yield return new WaitForSeconds(0.5f);

        isDead = false;
        playerController.canMove = true;
    }
}
