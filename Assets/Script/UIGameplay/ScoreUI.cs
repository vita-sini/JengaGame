using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI _scoreText; // Текстовый элемент для отображения очков

    private void OnEnable()
    {
        // подписываемся при каждом включении
        ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
        UpdateScoreText(ScoreManager.Instance.CurrentScore);
    }

    private void OnDisable()
    {
        // отписываемся, чтобы избежать утечки
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        _scoreText.text = $"{score}";

        if (score <= 0)
            return;

        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "GameplayNewChallenges")
        {
            YandexGame.NewLeaderboardScores("NewChallenges", score);
        }
        else if (currentScene.name == "GameplayClassic")
        {
            YandexGame.NewLeaderboardScores("Classic", score);
        }
    }

    public void CalculateScore(Vector3 initialPos, Vector3 currentPos, Rigidbody block)
    {
        //if (currentPos.y <= initialPos.y + 1) return;

        ScoreManager.Instance.Add(1);   // вместо AddScore
        GameEvents.InvokeTurnEnd();
    }
}
