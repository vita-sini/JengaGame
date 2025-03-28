using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IPauseManager
{
    [SerializeField] private GameObject[] _gameplayUIElements;
    [SerializeField] private GameObject _pauseMenuCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;

    private bool _isPaused = false;
    public bool IsPaused => _isPaused;

    private void Start()
    {
        _pauseMenuCanvas.SetActive(false);
        _settingsCanvas.SetActive(false);
    }

    private void Update()
    {
        // Проверяем нажатие кнопки "Menu" или клавиши Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (_isPaused)
        {
            return;
        }
    }

    public void TogglePauseMenu()
    {
        _isPaused = !_isPaused;
        _pauseMenuCanvas.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0 : 1; // Ставим игру на паузу или возобновляем

        foreach (var uiElement in _gameplayUIElements)
        {
            uiElement.SetActive(!_isPaused);
        }
    }

    public void ContinueGame()
    {
        TogglePauseMenu();
    }

    public void StartNewGame()
    {
        Time.timeScale = 1; // Возобновляем время

        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "GameplayNewChallenges")
        {
            SceneManager.LoadScene("GameplayNewChallenges");
        }
        else if (currentScene.name == "GameplayClassic")
        {
            SceneManager.LoadScene("GameplayClassic");
        }
    }

    public void OpenSettings()
    {
        _settingsCanvas.SetActive(true);
        _pauseMenuCanvas.SetActive(false);
    }


    public void CloseSettings()
    {
        _settingsCanvas.SetActive(false);
        _pauseMenuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MAINMENU");
    }
}
