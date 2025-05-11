using UnityEngine;

[CreateAssetMenu]
public class ItemSO: ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeSet;
    public AttributesToChange attributesToChange = new AttributesToChange();
    public int amountToChangeAttributes;

    public bool UseItem()
    {
        menucontroller menu = GameObject.Find("UI").GetComponent<menucontroller>();
        XuLyVaCham xuLyVaCham = menu.xuLyVaCham;

        if (statToChange == StatToChange.health)
        {
            if (xuLyVaCham.Hp == xuLyVaCham.maxHealth)
            {
                Debug.Log("Máu đã đầy");
                return false;
            }
            else
            {
                Debug.Log("Hồi máu");
                xuLyVaCham.RestoreHealth(amountToChangeSet);
                return true;
            }
        }
        return false;
    }

    public enum StatToChange
    {
        none,
        health,
        card
    };

    public enum AttributesToChange
    {
        none,
        health,
        card
    };
}
