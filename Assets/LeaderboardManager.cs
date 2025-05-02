using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    //public TextMeshProUGUI leaderboardText;
    //public Image medalImage;
   // public Sprite goldMedal, silverMedal, bronzeMedal;
    //public Button backButton;

   // private struct PlayerScore
    //{
        //public string name;
        //public int score;
       //public int level;

        //public PlayerScore(string name, int score, int level)
        //{
         //   this.name = name;
        //    this.score = score;
        //    this.level = level;
        //}
   // }

   // private void Start()
   // {
        // Tạo dữ liệu giả cho 5 người chơi
        //List<PlayerScore> leaderboard = new List<PlayerScore>
        //{
          //  new PlayerScore("Người Chơi 1", 50, 3),
         //   new PlayerScore("Người Chơi 2", 45, 3),
         //   new PlayerScore("Người Chơi 3", 40, 2),
         //   new PlayerScore("Người Chơi 4", 35, 2),
         //   new PlayerScore("Người Chơi 5", 30, 1)
        //};

        // Lấy dữ liệu người chơi hiện tại
        //int playerScore = GameManager.Instance.GetPlayerScore();
        //int playerLevel = GameManager.Instance.GetLastPlayedLevel();
        //PlayerScore currentPlayer = new PlayerScore("Bạn", playerScore, playerLevel);

        // Thêm người chơi vào danh sách và sắp xếp
       // leaderboard.Add(currentPlayer);
       // leaderboard.Sort((a, b) => b.score.CompareTo(a.score));

        // Hiển thị bảng xếp hạng
       // string leaderboardDisplay = "Bảng Xếp Hạng:\n";
       // int playerRank = -1;
      //  for (int i = 0; i < Mathf.Min(5, leaderboard.Count); i++)
       // {
      //      leaderboardDisplay += $"{i + 1}. {leaderboard[i].name} - Điểm: {leaderboard[i].score} (Màn {leaderboard[i].level})\n";
      //      if (leaderboard[i].name == "Bạn")
      //      {
      //          playerRank = i + 1;
      //      }
       // }
      //  leaderboardText.text = leaderboardDisplay;

        // Hiển thị huy chương nếu lọt top 3
      //  medalImage.gameObject.SetActive(false);
       // if (playerRank > 0 && playerRank <= 3)
       // {
       //     medalImage.gameObject.SetActive(true);
       //     if (playerRank == 1) medalImage.sprite = goldMedal;
       //     else if (playerRank == 2) medalImage.sprite = silverMedal;
       //     else if (playerRank == 3) medalImage.sprite = bronzeMedal;
        //}

        // Gán sự kiện cho nút Quay Lại
      //  backButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenuScene"));
    //}
}