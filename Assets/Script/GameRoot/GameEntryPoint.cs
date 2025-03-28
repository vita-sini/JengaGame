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
        // Показываем экран загрузки
        if (_loadingScreen != null)
        {
            _loadingScreen.SetActive(true);
        }

        float startTime = Time.time;

        // Начинаем асинхронную загрузку сцены главного меню
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scenes.MAIN_MENU);
        asyncLoad.allowSceneActivation = false; // Пока не даем активировать сцену

        // Пока загрузка не завершена
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // Обновляем прогресс-бар, если он есть
            if (_loadingBar != null)
            {
                _loadingBar.value = progress; // Изменяем значение слайдера
            }

            // Проверяем, прошло ли минимальное время и завершилась ли загрузка
            if (Time.time - startTime >= _minLoadTime && asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true; // Разрешаем активацию сцены
            }

            yield return null;
        }
    }
}
