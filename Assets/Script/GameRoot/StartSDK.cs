using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StartSDK : MonoBehaviour
{
    //private void OnEnable()
    //{
    //    YandexGame.GetDataEvent += GetLoad;
    //}

    //// Отписываемся от события GetDataEvent в OnDisable.
    //private void OnDisable()
    //{
    //    YandexGame.GetDataEvent -= GetLoad;
    //}

    private void Start()
    {
        //if (YandexGame.SDKEnabled == true) // Проверяем, запустился ли плагин.
        //{
        //    GetLoad();
        //}

        string lang = YandexGame.EnvironmentData.language;

        // Приводим к стандартному формату (например: "tr", "ru", "en")
        lang = lang.ToLower();

        LocalizationManager.SetLanguage(lang);
    }

    // Ваш метод для загрузки, который будет запускаться после загрузки SDK.
    //public void GetLoad()
    //{
    //    YandexGame.GameReadyAPI();
    //}
}
