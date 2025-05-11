using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    public int quantity;

    [SerializeField]
    public Sprite sprite;

    [TextArea]
    [SerializeField]
    public string itemDescription;

    private menucontroller menucontroller;

    void Start()
    {
        menucontroller = GameObject.Find("UI").GetComponent<menucontroller>();    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            menucontroller.AddItem(itemName, quantity, sprite, itemDescription);
            Destroy(gameObject);
        }
    }
}
