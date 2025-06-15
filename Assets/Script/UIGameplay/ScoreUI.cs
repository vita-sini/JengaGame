using Gameplay;
using GameRoot;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace UIGameplay
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private GameEvents _gameEvents;

        public TextMeshProUGUI _scoreText;

        private void OnEnable()
        {
            _scoreManager.ScoreChanged += UpdateScoreText;
            UpdateScoreText(_scoreManager.CurrentScore);
        }

        private void OnDisable()
        {
            if (_scoreManager != null)
                _scoreManager.ScoreChanged -= UpdateScoreText;
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
            _scoreManager.Add(1);
            _gameEvents.OnInvokeTurnEnd();
        }
    }
}
