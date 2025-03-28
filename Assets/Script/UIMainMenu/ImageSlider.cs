using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    [SerializeField] private Image[] _images; // Массив картинок
    [SerializeField] private Button _leftButton; // Кнопка влево
    [SerializeField] private Button _rightButton; // Кнопка вправо
    [SerializeField] private int _currentIndex = 0;

    private void Start()
    {
        // Убедитесь, что кнопки назначены
        if (_leftButton != null)
        {
            _leftButton.onClick.AddListener(OnLeftButtonClicked);
        }

        if (_rightButton != null)
        {
            _rightButton.onClick.AddListener(OnRightButtonClicked);
        }

        // Показываем первую картинку
        ShowImage(_currentIndex);
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
    }
}
