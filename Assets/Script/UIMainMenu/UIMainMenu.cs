using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _leaderboardCanvas;
    [SerializeField] private GameObject _shopCanvas;
    [SerializeField] private Button _newGameChallengesButton;
    [SerializeField] private Button _newGameClassicButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _closeSettingsButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _closeLeaderboardButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _closeShopButton;

    private void Start()
    {
        SetCanvasActive(_settingsCanvas, false);
        SetCanvasActive(_leaderboardCanvas, false);
        SetCanvasActive(_shopCanvas, false);

        // Привязываем методы к кнопкам
        _shopButton.onClick.AddListener(() => SetCanvasActive(_shopCanvas, true));
        _settingsButton.onClick.AddListener(() => SetCanvasActive(_settingsCanvas, true));
        _closeSettingsButton.onClick.AddListener(() => SetCanvasActive(_settingsCanvas, false));
        _closeShopButton.onClick.AddListener(() => SetCanvasActive(_shopCanvas, false));
        _leaderboardButton.onClick.AddListener(() => SetCanvasActive(_leaderboardCanvas, true));
        _closeLeaderboardButton.onClick.AddListener(() => SetCanvasActive(_leaderboardCanvas, false));
        _newGameChallengesButton.onClick.AddListener(StartNewGameChallenges);
        _newGameClassicButton.onClick.AddListener(StartNewGameClassic);
    }

    public void StartNewGameChallenges()
    {
        SceneManager.LoadScene("GAMEPLAYNEWCHALLENGES");
    }

    public void StartNewGameClassic()
    {
        SceneManager.LoadScene("GAMEPLAYCLASSIC");
    }

    private void SetCanvasActive(GameObject canvas, bool isActive)
    {
        canvas.SetActive(isActive);
    }
}
