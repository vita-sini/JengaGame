using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI _scoreText; // Текстовый элемент для отображения очков
    private int _currentScore = 0;

    public void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        _currentScore += points;
        UpdateScoreText();
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    private void UpdateScoreText()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (_scoreText != null)
        {
            _scoreText.text = "Очки: " + _currentScore;

            if (currentScene.name == "GameplayNewChallenges")
            {
                YandexGame.NewLeaderboardScores("NewChallenges", _currentScore);
            }
            else if (currentScene.name == "GameplayClassic")
            {
                YandexGame.NewLeaderboardScores("Classic", _currentScore);
            }
        }
    }

    public void CalculateScore(Vector3 initialPosition, Vector3 currentPosition, Rigidbody block)
    {
        // Если блок ниже начальной позиции, очки не начисляются
        if (currentPosition.y <= initialPosition.y + 1)
        {
            Debug.Log("Блок опущен ниже или не перемещён, очки не начисляются.");
            return;
        }

        // Если все условия выполнены, начисляем очки
        AddScore(1);
        Debug.Log("Ход засчитан.");
        GameEvents.InvokeTurnEnd();
    }
}
