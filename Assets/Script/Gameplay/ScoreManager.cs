using System;
using UnityEngine;

namespace Gameplay
{
    public class ScoreManager : MonoBehaviour
    {
        public int CurrentScore { get; private set; }

        public event Action<int> ScoreChanged;

        public void Add(int points)
        {
            CurrentScore += points;
            ScoreChanged?.Invoke(CurrentScore);
        }

        public void ResetScore()
        {
            CurrentScore = 0;
            ScoreChanged?.Invoke(CurrentScore);
        }
    }
}
