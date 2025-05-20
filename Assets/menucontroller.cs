using UnityEngine;

public class menucontroller : MonoBehaviour
{
    public GameObject menuCanvas;
    private bool menuActivated;
    public ItemSlot[] itemSlot;
    public ItemSO[] itemSOs;

    //public string saveFileName = "inventory_save.json";
    public XuLyVaCham xuLyVaCham;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            bool isActive = !menuCanvas.activeSelf;
            SoundEffectManager.Play("Book");
            menuCanvas.SetActive(isActive);
            Time.timeScale = isActive ? 0 : 1;
        }
    }

    public bool UseItem(string itemName)
    {
        for(int i = 0; i < itemSOs.Length; i++) 
        {
            if (itemSOs[i].itemName == itemName) 
            {
                Debug.Log("Dùng item: " + itemName);
                bool usable = itemSOs[i].UseItem();
                return usable;
            }
        }
        return false;
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        // 1. Tìm slot đã có cùng item để cộng dồn
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull && itemSlot[i].itemName == itemName)
            {
                itemSlot[i].quantity += quantity;
                itemSlot[i].UpdateQuantity(quantity); // nếu biến quantity có tồn tại ở hàm đó
                Debug.Log($"Đã cộng thêm {quantity} {itemName} vào slot hiện có.");
                return;
            }
        }

        // 2. Nếu chưa có, tìm slot trống để thêm mới
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                Debug.Log($"Đã thêm mới {itemName} vào slot {i}.");
                return;
            }
        }

        Debug.LogWarning("Không còn chỗ trống trong inventory!");
    }


    public void DeselectAllSlot()
    {
        for (int i = 0; i < itemSlot.Length;i++) 
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
