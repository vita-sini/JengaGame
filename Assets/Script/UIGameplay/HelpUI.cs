using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : MonoBehaviour
{
    [SerializeField] private GameObject _initialUIElement;
    [SerializeField] private GameObject[] _otherUIElements;

    public void CloseInitialUI()
    {
        _initialUIElement.SetActive(false);

        foreach (GameObject uiElement in _otherUIElements)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }
        }

        // Возвращаем игру в нормальное состояние
        Time.timeScale = 1f; // Возобновляет игровое время
    }

    public void OpenInitialUI()
    {
        _initialUIElement.SetActive(true);

        foreach (GameObject uiElement in _otherUIElements)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }

        // Ставим игру на паузу
        Time.timeScale = 0f; // Останавливает игровое время
    }
}
