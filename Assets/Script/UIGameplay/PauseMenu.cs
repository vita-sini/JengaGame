using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;

    private bool _isPaused = false;

    private void Start()
    {
        _pauseMenuCanvas.SetActive(false);
    }

    private void Update()
    {
        // ��������� ������� ������ "Menu" ��� ������� Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        _isPaused = !_isPaused;
        _pauseMenuCanvas.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0 : 1; // ������ ���� �� ����� ��� ������������
    }

    public void ContinueGame()
    {
        TogglePauseMenu();
    }

    public void StartNewGame()
    {
        Time.timeScale = 1; // ������������ �����
        SceneManager.LoadScene("BOOT"); 
        SceneManager.LoadScene("GAMEPLAY"); 
    }

    public void OpenSettings()
    {
        _settingsCanvas.SetActive(true);
    }


    public void CloseSettings()
    {
        // ������������ Canvas ��������
        _settingsCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MAINMENU");
    }
}
