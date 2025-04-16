using UnityEngine;
using UnityEngine.UI;
using YG;

public class ImageSlider : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private Material[] _materials;
    [SerializeField] private Button _leftButton; 
    [SerializeField] private Button _rightButton; 
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private string _paidProductId;

    private const string SelectedMaterialKey = "SelectedMaterialIndex";
    private int _currentIndex = 0;

    private void Start()
    {
        if (_leftButton != null)
            _leftButton.onClick.AddListener(OnLeftButtonClicked);

        if (_rightButton != null)
            _rightButton.onClick.AddListener(OnRightButtonClicked);

        if (_selectButton != null)
            _selectButton.onClick.AddListener(OnSelectButtonClicked);

        YandexGame.GetPaymentsEvent += OnPaymentsReceived;

        // Загружаем сохранённый индекс
        _currentIndex = YandexGame.savesData.selectedMaterialIndex;

        // Показываем первую картинку
        ShowImage(_currentIndex);

        UpdateBuyButtonState();
    }

    private void OnDestroy()
    {
        YandexGame.GetPaymentsEvent -= OnPaymentsReceived;
    }

    private void OnSelectButtonClicked()
    {
        // Сохраняем выбранный индекс
        YandexGame.savesData.selectedMaterialIndex = _currentIndex;
        YandexGame.SaveProgress();
        Debug.Log("Сохранение отправлено: selectedMaterialIndex = " + _currentIndex);
    }

    private void OnPaymentsReceived()
    {
        Debug.Log("OnPaymentsReceived: Данные о покупках получены.");
        // Обновляем кнопку, когда SDK получит данные о покупках
        UpdateSelectButtonState();
        UpdateBuyButtonState();
    }

    private void OnLeftButtonClicked()
    {
        _currentIndex = (_currentIndex - 1 + _images.Length) % _images.Length;
        ShowImage(_currentIndex);
    }

    private void OnRightButtonClicked()
    {
        _currentIndex = (_currentIndex + 1) % _images.Length;
        ShowImage(_currentIndex);
    }

    private void ShowImage(int index)
    {
        for (int i = 0; i < _images.Length; i++)
        {
            _images[i].gameObject.SetActive(i == index);
        }

        UpdateSelectButtonState();
    }

    private void UpdateSelectButtonState()
    {
        Debug.Log("UpdateSelectButtonState вызван!");

        if (_currentIndex == 0)
        {
            _selectButton.interactable = true;
            Debug.Log("Первый материал, кнопка активна.");
            return;
        }

        var purchase = YandexGame.PurchaseByID(_paidProductId);

        if (purchase != null && purchase.consumed == true)
        {
            _selectButton.interactable = true;
            Debug.Log("Покупка подтверждена, кнопка активна.");
        }
        else
        {
            SetButtonState(_selectButton, false);
            Debug.Log("Покупка не подтверждена, кнопка не активна.");
        }

        // Обновляем кнопку "Купить"
        UpdateBuyButtonState();
    }

    private void UpdateBuyButtonState()
    {
        if (_buyButton == null)
            return;

        // Проверяем, куплен ли товар
        var purchase = YandexGame.PurchaseByID(_paidProductId);
        bool isPurchased = purchase != null && purchase.consumed;

        // Кнопка остаётся видимой, но становится неактивной, если куплено
        _buyButton.interactable = !isPurchased;
        SetButtonState(_buyButton, !isPurchased); 
    }

    private void SetButtonState(Button button, bool isEnabled)
    {
        button.interactable = isEnabled;

        ColorBlock colors = button.colors;

        if (isEnabled)
        {
            colors.normalColor = Color.white; // Активная — белая
        }
        else
        {
            colors.normalColor = new Color(1f, 1f, 1f, 0.5f); 
        }

        button.colors = colors;
    }
}
