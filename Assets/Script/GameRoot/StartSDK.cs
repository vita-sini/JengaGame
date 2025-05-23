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
        if (YandexGame.SDKEnabled == true) // ���������, ���������� �� ������.
        {
            GetLoad();
        }

        string lang = YandexGame.EnvironmentData.language;

        //�������� � ������������ �������(��������: "tr", "ru", "en")
        lang = lang.ToLower();

        LocalizationManager.SetLanguage(lang);
    }

    public void GetLoad(){ YandexGame.GameReadyAPI();}
}
