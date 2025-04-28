using UnityEngine;
using TMPro;
using System.Collections;

public class XuLyVaCham : MonoBehaviour
{
    public int Hp = 3;
    public int Star = 0;
    public TextMeshProUGUI heartText;
    public TextMeshProUGUI StarText;
    public Animator animator;
    public Dc playerController;

    private Vector2 spawnPoint;
    private bool isDead = false;
    // để GameManager có thể truy cập
    public GameObject currentTriggerObject;

    void Start()
    {
        spawnPoint = transform.position;
        StarText.SetText(Star.ToString());
        heartText.SetText(Hp.ToString());
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Vang") || colision.CompareTag("enemy"))
        {
            currentTriggerObject = colision.gameObject;
            GameManager.Instance.TriggerQuestion(currentTriggerObject);
        }
        else if (colision.CompareTag("GaiNhon"))
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

    public void TakeDamage()
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

    public void CollectItem()
    {
        Inventory.Instance.AddItem();
        StarText.SetText(Inventory.Instance.GetItemCount().ToString());
        Destroy(currentTriggerObject);
        currentTriggerObject = null;
    }

    public void SkipItem()
    {
        Destroy(currentTriggerObject);
        currentTriggerObject = null;
    }

    public void DefeatEnemy()
    {
        if (currentTriggerObject != null)
        {
            Destroy(currentTriggerObject);
            currentTriggerObject = null;
        }
    }

    IEnumerator DieAndRespawn()
    {
        isDead = true;
        playerController.canMove = false;
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(1.5f);

        transform.position = spawnPoint;
        Hp = 3;
        heartText.SetText(Hp.ToString());

        yield return new WaitForSeconds(0.5f);

        isDead = false;
        playerController.canMove = true;
    }
}