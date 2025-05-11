using UnityEngine;
using UnityEngine.UI;

public class tabcontroller : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateTab(0);
    }

    // Update is called once per frame
    public void ActivateTab(int tabNo)
    {
       for(int i = 0; i < tabImages.Length; i++) 
       {
            pages[i].SetActive(false);
            tabImages[i].color = Color.gray;
       }
        pages[tabNo].SetActive(true);
        tabImages[tabNo].color = Color.white;
    }
}
