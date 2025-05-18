using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnSettingsButtonClicked()
    {
        SceneManager.LoadScene("StatisticsScene");
    }

    public void OnLeaderboardButtonClicked()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }
    public void OnBackToMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}