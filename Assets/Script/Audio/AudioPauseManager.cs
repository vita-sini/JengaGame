using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AudioPauseManager : MonoBehaviour
{
    private void OnEnable()
    {
        YandexGame.onShowWindowGame += OnShowWindowGame; // ������������� �� ��������
        YandexGame.onHideWindowGame += OnHideWindowGame; // ������������� �� ��������
    }
    private void OnDisable()
    {
        YandexGame.onShowWindowGame -= OnShowWindowGame; // ������������ �� ��������
        YandexGame.onHideWindowGame -= OnHideWindowGame; // ������������ �� ��������
    }

    void OnShowWindowGame()
    {
        AudioListener.pause = false;// ���� ������ ��� �������� ������� ����
    }
    void OnHideWindowGame()
    {
        AudioListener.pause = true; // ���� ������ ��� �������� ������� ����
    }
}
