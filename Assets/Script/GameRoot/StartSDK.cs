using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StartSDK : MonoBehaviour
{
    [SerializeField] private float _minLoadTime = 2f;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingBar;

    private GameEntryPoint _gameEntryPoint;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    // ������������ �� ������� GetDataEvent � OnDisable.
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void Awake()
    {
        _gameEntryPoint = new GameEntryPoint(_minLoadTime, _loadingScreen, _loadingBar);
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true) // ���������, ���������� �� ������.
        {
            GetLoad();
        }
    }

    // ��� ����� ��� ��������, ������� ����� ����������� ����� �������� SDK.
    public void GetLoad()
    {
        StartCoroutine(_gameEntryPoint.LoadGame());
    }
}
