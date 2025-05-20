using System.Collections;
using TMPro;
using UnityEngine;

public class XuLyVaCham : MonoBehaviour
{
    public int Hp = 3;
    public int maxHealth = 3;
    public int Star = 0;
    public TextMeshProUGUI heartText;
    public TextMeshProUGUI StarText;
    public Animator animator;
    public Dc playerController;
    private Vector2 spawnPoint;
    private bool isDead = false;
    public bool isAnsweringQuestion = false; //trạng thái đang hoạt động
    void Start()
    {
        spawnPoint = transform.position;
        StarText.SetText(Star.ToString());
        heartText.SetText(Hp.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAnsweringQuestion) return;
        Debug.Log($"Va chạm với: {collision.tag}");
        if (collision.CompareTag("Vang"))
        {
            Star++;
            StarText.SetText(Star.ToString());
            SoundEffectManager.Play("Star");
            Destroy(collision.gameObject);
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TriggerQuestion(collision.gameObject);
            }
            else
            {
                Debug.LogError("GameManager.Instance is null!");
            }
        }
        else if (collision.CompareTag("GaiNhon") || collision.CompareTag("enemy"))
        {
            Hp--;
            heartText.SetText(Hp.ToString());

            if (Hp > 0)
            {
                SoundEffectManager.Play("PlayerHurt");
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

        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuMain");
    }
}