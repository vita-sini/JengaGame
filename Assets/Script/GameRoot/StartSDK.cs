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

    //// ������������ �� ������� GetDataEvent � OnDisable.
    //private void OnDisable()
    //{
    //    YandexGame.GetDataEvent -= GetLoad;
    //}

    private void Start()
    {
        //if (YandexGame.SDKEnabled == true) // ���������, ���������� �� ������.
        //{
        //    GetLoad();
        //}

        string lang = YandexGame.EnvironmentData.language;

        // �������� � ������������ ������� (��������: "tr", "ru", "en")
        lang = lang.ToLower();

        LocalizationManager.SetLanguage(lang);
    }

    // ��� ����� ��� ��������, ������� ����� ����������� ����� �������� SDK.
    //public void GetLoad()
    //{
    //    YandexGame.GameReadyAPI();
    //}
}
