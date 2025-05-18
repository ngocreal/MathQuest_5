using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Quest", menuName = "Math/Quest")]
public class Quest : ScriptableObject
{
    public string questionText; //câu hỏi
    public string imagePath; // url ảnh
    public string correctAnswer; //trả lời đug
    public bool isMultipleChoice; //trắc nghiệm?
    public string[] choices; //4 đ.an
    public int difficulty; //mức độ
    public int pointsReward; // Điểm thưởng khi trả lời đúng
}

