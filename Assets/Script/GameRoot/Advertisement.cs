using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public static class Advertisement 
{
    private static string _sceneToLoad;
    private static bool _isAdShown;

    public static void LoadSceneWithAd(string sceneName)
    {
        if (_isAdShown)
        {
            Debug.Log("Реклама уже показывается. Прерываем повторный вызов.");
            return;
        }

        _sceneToLoad = sceneName;
        _isAdShown = true;

        Debug.Log($"Попытка показать рекламу перед сценой: {_sceneToLoad}");

#if UNITY_EDITOR
        Debug.Log("Editor mode: реклама не показывается, загружаем сцену напрямую.");
        LoadSceneAsync();
        return;
#endif

        YandexGame.CloseFullAdEvent += OnAdClosed;
        YandexGame.ErrorFullAdEvent += OnAdFailed;

        YandexGame.FullscreenShow();

        Debug.Log("Вызван YandexGame.FullscreenShow()");
    }

    private static void OnAdClosed()
    {
        Debug.Log("Событие: реклама закрыта");
        CleanupEvents();
        LoadSceneAsync();
    }

    private static void OnAdFailed()
    {
        Debug.LogWarning("Событие: реклама не была показана. Загружаем сцену без неё.");
        CleanupEvents();
        LoadSceneAsync();
    }

    private static void CleanupEvents()
    {
        Debug.Log("Очистка событий рекламы");
        YandexGame.CloseFullAdEvent -= OnAdClosed;
        YandexGame.ErrorFullAdEvent -= OnAdFailed;
        _isAdShown = false;
    }

    private static async void LoadSceneAsync()
    {
        Debug.Log($"Асинхронная загрузка сцены: {_sceneToLoad}");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneToLoad);
        while (!asyncLoad.isDone)
        {
            await System.Threading.Tasks.Task.Yield();
        }
        Debug.Log("Сцена загружена.");
    }
}
