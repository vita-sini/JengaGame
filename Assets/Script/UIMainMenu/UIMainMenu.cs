using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _closeSettingsButton;

    private void Start()
    {
        // Привязываем методы к кнопкам
        _newGameButton.onClick.AddListener(StartNewGame);
        _settingsButton.onClick.AddListener(OpenSettings);
        _closeSettingsButton.onClick.AddListener(CloseSettings);

        // Убедимся, что Canvas настроек изначально неактивен
        _settingsCanvas.SetActive(false);
    }

    public void StartNewGame()
    {
        // Загружаем сцену игрового процесса
        SceneManager.LoadScene("GAMEPLAY");
    }

    public void OpenSettings()
    {
        // Активируем Canvas настроек
        _settingsCanvas.SetActive(true);
    }

    public void CloseSettings()
    {
        // Деактивируем Canvas настроек
        _settingsCanvas.SetActive(false);
    }
}
