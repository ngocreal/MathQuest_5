using UnityEngine;

public class Enemy_behavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Ph??ng th?c ?? x? l� khi k? th� b? ?�nh b?i
    public GameObject smokeEffect; // Hi?u ?ng kh�i
    public AudioClip defeatSound;

    public void Defeat()
    {
        if (smokeEffect != null)
            Instantiate(smokeEffect, transform.position, Quaternion.identity);
        if (defeatSound != null)
            AudioSource.PlayClipAtPoint(defeatSound, transform.position);
    }
}