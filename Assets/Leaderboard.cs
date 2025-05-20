using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private Transform rowContainer;
    [SerializeField] private TextMeshProUGUI playerRankText;

    void Start()
    {
        // Mock dữ liệu
        (string name, int score)[] topPlayers = {
            ("Player A", 100),
            ("Player B", 80),
            ("Player C", 60),
            ("Player D", 40),
            ("Player E", 20)
        };

        int currentPlayerScore = 50; // Mock điểm người chơi hiện tại
        DisplayLeaderboard(topPlayers, currentPlayerScore);
    }

    void DisplayLeaderboard((string name, int score)[] topPlayers, int currentPlayerScore)
    {
        // Tạo danh sách top 5
        for (int i = 0; i < topPlayers.Length; i++)
        {
            GameObject row = Instantiate(rowPrefab, rowContainer);
            TextMeshProUGUI[] texts = row.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = (i + 1).ToString(); // Stt
            texts[1].text = topPlayers[i].name;  // Tên
            texts[2].text = topPlayers[i].score.ToString(); // Điểm

            
            Image background = row.GetComponent<Image>();
            if (i == 0) background.color = new Color(1f, 0.84f, 0f); // Vàng
            else if (i == 1) background.color = Color.gray; // Bạc
            else if (i == 2) background.color = new Color(0.8f, 0.5f, 0.2f); // Đồng
            else background.color = Color.gray; // Xám

            Button button = row.GetComponent<Button>();
            if (button != null)
            {
                ColorBlock colors = button.colors;
                colors.highlightedColor = new Color(0.5f, 0.5f, 0.5f); // Xám đậm
                button.colors = colors;
            }
        }

        // Xếp hạng người chơi hiện tại
        int rank = topPlayers.Length + 1;
        for (int i = 0; i < topPlayers.Length; i++)
        {
            if (currentPlayerScore > topPlayers[i].score)
            {
                rank = i + 1;
                break;
            }
        }
        playerRankText.text = "Xếp hạng của bạn: " + rank;
    }
}