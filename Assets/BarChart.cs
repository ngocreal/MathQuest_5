using UnityEngine;
using UnityEngine.UI;

public class BarChart : MonoBehaviour
{
    [SerializeField] private GameObject barPrefab;

    void Start()
    {
        // Mock dữ liệu
        PlayerPrefs.SetInt("Level1_Correct", 4);
        PlayerPrefs.SetInt("Level1_Incorrect", 2);
        PlayerPrefs.SetInt("Level2_Correct", 8);
        PlayerPrefs.SetInt("Level2_Incorrect", 3);
        PlayerPrefs.SetInt("Level3_Correct", 12);
        PlayerPrefs.SetInt("Level3_Incorrect", 5);
        PlayerPrefs.Save();

        UpdateChart();
    }

    void UpdateChart()
    {
        string[] levels = { "Level1", "Level2", "Level3" };
        for (int i = 0; i < levels.Length; i++)
        {
            int correct = PlayerPrefs.GetInt(levels[i] + "_Correct", 0);
            int incorrect = PlayerPrefs.GetInt(levels[i] + "_Incorrect", 0);

            // Cột đúng
            GameObject correctBar = Instantiate(barPrefab, transform);
            correctBar.GetComponent<Image>().color = Color.green;
            correctBar.GetComponent<RectTransform>().sizeDelta = new Vector2(20, correct * 10);
            correctBar.GetComponent<RectTransform>().localPosition = new Vector3(i * 60 - 10, correct * 5, 0);

            // Cột sai
            GameObject incorrectBar = Instantiate(barPrefab, transform);
            incorrectBar.GetComponent<Image>().color = Color.red;
            incorrectBar.GetComponent<RectTransform>().sizeDelta = new Vector2(20, incorrect * 10);
            incorrectBar.GetComponent<RectTransform>().localPosition = new Vector3(i * 60 + 10, incorrect * 5, 0);
        }
    }
}