using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //Item date
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    //item slot
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    //item description slot
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private menucontroller menucontroller;

    private void Start()
    {
        menucontroller = GameObject.Find("UI").GetComponent<menucontroller>();
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick() 
    {
        if(thisItemSelected)
        {
            bool usable = menucontroller.UseItem(itemName);
            if(usable) 
            { 
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if(this.quantity <= 0)
                {
                     EmptySlot();
                }
            }
            
        }
        else
        { 
            menucontroller.DeselectAllSlot();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
            if(itemDescriptionImage.sprite == null) 
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }
        
    }

    public void UpdateQuantity(int newQuantity)
    {
        this.quantity = newQuantity;
        quantityText.text = quantity.ToString();
    }

    private void EmptySlot()
    {
       quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
    }

    public void OnRightClick() 
    { 

    }
}
