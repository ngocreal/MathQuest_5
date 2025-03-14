using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Weapon, Key, Heart }
    public ItemType itemType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (itemType)
            {
                case ItemType.Heart:
                    PlayerHealth.Instance.AddHeart(); // Tăng cơ hội trả lời
                    break;
                case ItemType.Weapon:
                    PlayerInventory.Instance.AddWeapon();
                    break;
                case ItemType.Key:
                    PlayerInventory.Instance.AddKey();
                    break;
            }

            Question randomQuestion = QuestionManager.Instance.GetRandomQuestion(GameManager.Instance.GetDifficultyLevel());
            UIManager.Instance.ShowQuestionPanel(randomQuestion);
            Destroy(gameObject); // Xóa vật phẩm sau khi nhặt
        }
    }
}
