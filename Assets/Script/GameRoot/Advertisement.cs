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
            Debug.Log("������� ��� ������������. ��������� ��������� �����.");
            return;
        }

        _sceneToLoad = sceneName;
        _isAdShown = true;

        Debug.Log($"������� �������� ������� ����� ������: {_sceneToLoad}");

#if UNITY_EDITOR
        Debug.Log("Editor mode: ������� �� ������������, ��������� ����� ��������.");
        LoadSceneAsync();
        return;
#endif

        YandexGame.CloseFullAdEvent += OnAdClosed;
        YandexGame.ErrorFullAdEvent += OnAdFailed;

        YandexGame.FullscreenShow();

        Debug.Log("������ YandexGame.FullscreenShow()");
    }

    private static void OnAdClosed()
    {
        Debug.Log("�������: ������� �������");
        CleanupEvents();
        LoadSceneAsync();
    }

    private static void OnAdFailed()
    {
        Debug.LogWarning("�������: ������� �� ���� ��������. ��������� ����� ��� ��.");
        CleanupEvents();
        LoadSceneAsync();
    }

    private static void CleanupEvents()
    {
        Debug.Log("������� ������� �������");
        YandexGame.CloseFullAdEvent -= OnAdClosed;
        YandexGame.ErrorFullAdEvent -= OnAdFailed;
        _isAdShown = false;
    }

    private static async void LoadSceneAsync()
    {
        Debug.Log($"����������� �������� �����: {_sceneToLoad}");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneToLoad);
        while (!asyncLoad.isDone)
        {
            await System.Threading.Tasks.Task.Yield();
        }
        Debug.Log("����� ���������.");
    }
}
