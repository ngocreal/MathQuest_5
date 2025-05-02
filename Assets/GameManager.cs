using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public QuestDatabase questionDatabase;
   // public QuestUI1 questUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TriggerQuestion(GameObject triggerObject)
    {
        int difficulty = 1;

        if (triggerObject.CompareTag("enemy"))
            difficulty = 2;
        else if (triggerObject.CompareTag("Vang"))
            difficulty = 1;
    }

}
