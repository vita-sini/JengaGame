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
        // ����������� ������ � �������
        _newGameButton.onClick.AddListener(StartNewGame);
        _settingsButton.onClick.AddListener(OpenSettings);
        _closeSettingsButton.onClick.AddListener(CloseSettings);

        // ��������, ��� Canvas �������� ���������� ���������
        _settingsCanvas.SetActive(false);
    }

    public void StartNewGame()
    {
        // ��������� ����� �������� ��������
        SceneManager.LoadScene("GAMEPLAY");
    }

    public void OpenSettings()
    {
        // ���������� Canvas ��������
        _settingsCanvas.SetActive(true);
    }

    public void CloseSettings()
    {
        // ������������ Canvas ��������
        _settingsCanvas.SetActive(false);
    }
}
