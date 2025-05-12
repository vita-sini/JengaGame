using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AudioPauseManager : MonoBehaviour
{
    private void OnEnable()
    {
        YandexGame.onShowWindowGame += OnShowWindowGame; // Подписываемся на открытие
        YandexGame.onHideWindowGame += OnHideWindowGame; // Подписываемся на закрытие
    }
    private void OnDisable()
    {
        YandexGame.onShowWindowGame -= OnShowWindowGame; // Отписываемся от открытия
        YandexGame.onHideWindowGame -= OnHideWindowGame; // Отписываемся от закрытия
    }

    private void OnShowWindowGame()
    {
        AudioListener.pause = false;// Ваша логика при открытии вкладки игры
    }
    private void OnHideWindowGame()
    {
        AudioListener.pause = true; // Ваша логика при закрытии вкладки игры
    }
}
