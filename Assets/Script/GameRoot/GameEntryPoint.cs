using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEntryPoint
{
     private float _minLoadTime = 2f; 
     private GameObject _loadingScreen; 
     private Slider _loadingBar;

    public GameEntryPoint(float minLoadTime, GameObject loadingScreen, Slider loadingBar)
    {
        _minLoadTime = minLoadTime;
        _loadingScreen = loadingScreen;
        _loadingBar = loadingBar;
    }

    public IEnumerator LoadGame()
    {
        // ���������� ����� ��������
        if (_loadingScreen != null)
        {
            _loadingScreen.SetActive(true);
        }

        float startTime = Time.time;

        // �������� ����������� �������� ����� �������� ����
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scenes.MAIN_MENU);
        asyncLoad.allowSceneActivation = false; // ���� �� ���� ������������ �����

        // ���� �������� �� ���������
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // ��������� ��������-���, ���� �� ����
            if (_loadingBar != null)
            {
                _loadingBar.value = progress; // �������� �������� ��������
            }

            // ���������, ������ �� ����������� ����� � ����������� �� ��������
            if (Time.time - startTime >= _minLoadTime && asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true; // ��������� ��������� �����
            }

            yield return null;
        }
    }
}
