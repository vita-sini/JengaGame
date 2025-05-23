using UnityEngine;
using YG;

public class AudioPauseManager : MonoBehaviour
{
    private void OnEnable()
    {
        YandexGame.onShowWindowGame += OnShowWindowGame; 
        YandexGame.onHideWindowGame += OnHideWindowGame; 
    }
    private void OnDisable()
    {
        YandexGame.onShowWindowGame -= OnShowWindowGame; 
        YandexGame.onHideWindowGame -= OnHideWindowGame; 
    }

    private void OnShowWindowGame()
    {
        AudioListener.pause = false;
    }
    private void OnHideWindowGame()
    {
        AudioListener.pause = true; 
    }
}
