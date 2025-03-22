[System.Serializable]
public class Question
{
    public string questionText; // Nội dung câu hỏi
    public string imagePath; // Đường dẫn ảnh (nếu có)
    public string correctAnswer; // Đáp án đúng
    public bool isMultipleChoice; // True: trắc nghiệm, False: tự luận
    public string[] choices; // Danh sách đáp án (trắc nghiệm)
    public int difficulty; // Mức độ khó (1-3)
}
