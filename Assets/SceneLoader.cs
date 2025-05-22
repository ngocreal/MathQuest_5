using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu"); // Đổi tên đúng theo tên scene của bạn
    }
}

