using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUIElement;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        GameEvents.OnGameOver += ShowGameOverUI;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= ShowGameOverUI;

    }

    private void Start()
    {
        _gameOverUIElement.SetActive(false);
        _mainMenuButton.onClick.AddListener(LoadMainMenu);
        _restartButton.onClick.AddListener(RestartGame);
    }

    private void ShowGameOverUI()
    {
        _gameOverUIElement.SetActive(true);
        Time.timeScale = 0f;
        BlockOtherUIInteraction();
    }

    private void BlockOtherUIInteraction()
    {
        // Найдите все Canvas в сцене и отключите их взаимодействие
        Canvas[] canvases = FindObjectsOfType<Canvas>();
        foreach (var canvas in canvases)
        {
            if (canvas != _gameOverUIElement.GetComponent<Canvas>())
            {
                canvas.enabled = false;
            }
        }

    }
    private void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        Time.timeScale = 1f; 

        if (currentScene.name == "GameplayNewChallenges")
        {
            SceneManager.LoadScene("GameplayNewChallenges");
        }
        else if (currentScene.name == "GameplayClassic")
        {
            SceneManager.LoadScene("GameplayClassic");
        }
    }
}
