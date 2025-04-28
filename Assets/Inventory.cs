using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    private int collectedItems = 0; // Số lượng vật phẩm đã thu thập

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem()
    {
        collectedItems++;
        Debug.Log($"Đã thu thập {collectedItems} vật phẩm vào balo!");
    }

    public int GetItemCount()
    {
        return collectedItems;
    }
}