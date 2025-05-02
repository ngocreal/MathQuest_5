using UnityEngine;

public class DifficultySettings : MonoBehaviour
{
    public static Difficulty SelectedDifficulty = Difficulty.Easy;
    public void SetEasy() => SelectedDifficulty = Difficulty.Easy;
    public void SetMedium() => SelectedDifficulty = Difficulty.Medium;
    public void SetHard() => SelectedDifficulty = Difficulty.Hard;
}

