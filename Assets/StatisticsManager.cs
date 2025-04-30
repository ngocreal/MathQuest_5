using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StatisticsManager : MonoBehaviour
{
    public RawImage graphImage;
    public Button backButton;

    private Texture2D graphTexture;
    private int graphWidth = 600;
    private int graphHeight = 400;
    private int maxLevelsToShow = 0;
    private int maxAnswers = 10; // Giả sử số câu tối đa trên trục tung là 10

    private void Start()
    {
        // Tạo texture để vẽ biểu đồ
        graphTexture = new Texture2D(graphWidth, graphHeight);
        graphImage.texture = graphTexture;
        ClearGraph();

        // Lấy dữ liệu và vẽ biểu đồ
        DrawGraph();

        // Gán sự kiện cho nút Quay Lại
        backButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenuScene"));
    }

    private void ClearGraph()
    {
        for (int x = 0; x < graphWidth; x++)
        {
            for (int y = 0; y < graphHeight; y++)
            {
                graphTexture.SetPixel(x, y, Color.white);
            }
        }
        graphTexture.Apply();
    }

    private void DrawGraph()
    {
        int lastPlayedLevel = GameManager.Instance.GetLastPlayedLevel();
        int week = GameManager.Instance.GetLastPlayedWeek();
        maxLevelsToShow = lastPlayedLevel;

        List<int> correctAnswers = new List<int>();
        List<int> wrongAnswers = new List<int>();

        // Lấy dữ liệu số câu đúng/sai từ màn 1 đến màn hiện tại
        for (int level = 1; level <= maxLevelsToShow; level++)
        {
            int correct = GameManager.Instance.GetCorrectAnswers(level, week);
            int wrong = GameManager.Instance.GetWrongAnswers(level, week);
            correctAnswers.Add(correct);
            wrongAnswers.Add(wrong);
            maxAnswers = Mathf.Max(maxAnswers, correct + wrong);
        }

        // Vẽ trục
        DrawLine(50, 50, 50, graphHeight - 50, Color.black); // Trục tung
        DrawLine(50, 50, graphWidth - 50, 50, Color.black); // Trục hoành

        // Vẽ nhãn trục hoành (màn chơi)
        for (int level = 1; level <= maxLevelsToShow; level++)
        {
            int x = 50 + (level - 1) * (graphWidth - 100) / Mathf.Max(1, maxLevelsToShow - 1);
            DrawText($"Màn {level}", x, 30);
        }

        // Vẽ nhãn trục tung (số câu)
        for (int i = 0; i <= maxAnswers; i += 2)
        {
            int y = 50 + i * (graphHeight - 100) / maxAnswers;
            DrawText(i.ToString(), 30, y);
        }

        // Vẽ đường cho số câu đúng (màu xanh)
        for (int level = 1; level < maxLevelsToShow; level++)
        {
            int x1 = 50 + (level - 1) * (graphWidth - 100) / Mathf.Max(1, maxLevelsToShow - 1);
            int y1 = 50 + (correctAnswers[level - 1] * (graphHeight - 100) / maxAnswers);
            int x2 = 50 + level * (graphWidth - 100) / Mathf.Max(1, maxLevelsToShow - 1);
            int y2 = 50 + (correctAnswers[level] * (graphHeight - 100) / maxAnswers);
            DrawLine(x1, y1, x2, y2, Color.green);
        }

        // Vẽ đường cho số câu sai (màu đỏ)
        for (int level = 1; level < maxLevelsToShow; level++)
        {
            int x1 = 50 + (level - 1) * (graphWidth - 100) / Mathf.Max(1, maxLevelsToShow - 1);
            int y1 = 50 + (wrongAnswers[level - 1] * (graphHeight - 100) / maxAnswers);
            int x2 = 50 + level * (graphWidth - 100) / Mathf.Max(1, maxLevelsToShow - 1);
            int y2 = 50 + (wrongAnswers[level] * (graphHeight - 100) / maxAnswers);
            DrawLine(x1, y1, x2, y2, Color.red);
        }

        graphTexture.Apply();
    }

    private void DrawLine(int x1, int y1, int x2, int y2, Color color)
    {
        int dx = Mathf.Abs(x2 - x1);
        int dy = Mathf.Abs(y2 - y1);
        int sx = x1 < x2 ? 1 : -1;
        int sy = y1 < y2 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            if (x1 >= 0 && x1 < graphWidth && y1 >= 0 && y1 < graphHeight)
            {
                graphTexture.SetPixel(x1, y1, color);
            }

            if (x1 == x2 && y1 == y2) break;
            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x1 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y1 += sy;
            }
        }
    }

    private void DrawText(string text, int x, int y)
    {
        Debug.Log($"Nhãn tại ({x}, {y}): {text}");
    }
}