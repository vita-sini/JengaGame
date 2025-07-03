using UnityEngine;
using YG;

public class StartSDK : MonoBehaviour
{
    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true) // ѕровер€ем, запустилс€ ли плагин.
        {
            GetLoad();
        }

        string lang = YandexGame.EnvironmentData.language;

        //ѕриводим к стандартному формату(например: "tr", "ru", "en")
        lang = lang.ToLower();

        LocalizationManager.SetLanguage(lang);
    }

    public void GetLoad(){ YandexGame.GameReadyAPI();}
}
