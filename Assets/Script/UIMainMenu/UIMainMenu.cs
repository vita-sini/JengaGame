using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _leaderboardCanvas;
    [SerializeField] private GameObject _shopCanvas;
    [SerializeField] private GameObject _authorizationPanel;

    [SerializeField] private Button _newGameChallengesButton;
    [SerializeField] private Button _newGameClassicButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _closeSettingsButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _closeLeaderboardButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _closeShopButton;
    [SerializeField] private Button _authorizatioButton;         
    [SerializeField] private Button _authorizatioCancelButton;

    private void Start()
    {
        SetCanvasActive(_settingsCanvas, false);
        SetCanvasActive(_leaderboardCanvas, false);
        SetCanvasActive(_shopCanvas, false);
        SetCanvasActive(_authorizationPanel, false);

        // Привязываем методы к кнопкам
        _shopButton.onClick.AddListener(() => SetCanvasActive(_shopCanvas, true));
        _settingsButton.onClick.AddListener(() => SetCanvasActive(_settingsCanvas, true));
        _closeSettingsButton.onClick.AddListener(() => SetCanvasActive(_settingsCanvas, false));
        _closeShopButton.onClick.AddListener(() => SetCanvasActive(_shopCanvas, false));
        _leaderboardButton.onClick.AddListener(OnLeaderboardClick);
        _closeLeaderboardButton.onClick.AddListener(() => SetCanvasActive(_leaderboardCanvas, false));
        _newGameChallengesButton.onClick.AddListener(StartNewGameChallenges);
        _newGameClassicButton.onClick.AddListener(StartNewGameClassic);
        _authorizatioButton.onClick.AddListener(OnAuthPromptAuth);
        _authorizatioCancelButton.onClick.AddListener(() => SetCanvasActive(_authorizationPanel, false));

        // подписываемся на событие SDK – вызывается, когда игрок залогинен и данные получены
        YandexGame.GetDataEvent += OnYandexLoggedIn;
    }

    private void OnDestroy()
    {
        YandexGame.GetDataEvent -= OnYandexLoggedIn;
    }

    private void OnLeaderboardClick()
    {
        if (YandexGame.auth)                
            SetCanvasActive(_leaderboardCanvas, true);
        else                                
            SetCanvasActive(_authorizationPanel, true);
    }

    private void OnAuthPromptAuth()
    {
        // Показываем нативное окно авторизации Яндекса
        YandexGame.AuthDialog();
    }

    private void OnYandexLoggedIn()
    {
        if (!YandexGame.auth) return;

        // Если окно запроса авторизации было открыто – прячем его
        if (_authorizationPanel.activeSelf)
            SetCanvasActive(_authorizationPanel, false);

        // И сразу открываем лидерборд
        SetCanvasActive(_leaderboardCanvas, true);
    }

    public void StartNewGameChallenges()
    {
        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene("GAMEPLAYNEWCHALLENGES");
    }

    public void StartNewGameClassic()
    {
        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene("GAMEPLAYCLASSIC");
    }

    private void SetCanvasActive(GameObject canvas, bool isActive)
    {
        canvas.SetActive(isActive);
    }
}
