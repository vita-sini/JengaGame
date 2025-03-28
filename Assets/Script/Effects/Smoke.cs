using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour, IEffect
{
    [SerializeField] private AudioClip _smokeSound;
    [SerializeField] private ParticleSystem _smokeParticles;
    [SerializeField] private float _offsetY;

    private ParticleSystem _smokeParticlesInstance;
    private AudioSource _audioSource;
    private Camera _mainCamera;
    private Coroutine _smokeEffectCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _mainCamera = Camera.main;
        GameEvents.OnTurnEnd += Stop;
    }

    private void OnDisable()
    {
        GameEvents.OnTurnEnd -= Stop;
    }

    public void Execute()
    {
        if (_smokeEffectCoroutine != null)
        {
            StopCoroutine(_smokeEffectCoroutine);
        }
        _smokeEffectCoroutine = StartCoroutine(SmokeEffectCoroutine());
    }

    public void Stop()
    {
        if (_smokeEffectCoroutine != null)
        {
            StopCoroutine(_smokeEffectCoroutine);
            _smokeEffectCoroutine = null;
        }

        if (_smokeParticlesInstance != null)
        {
            _smokeParticlesInstance.Stop(); // Останавливаем инстанс системы частиц
            Destroy(_smokeParticlesInstance.gameObject, 1f); // Уничтожаем объект через 1 секунду (после завершения эффекта)
            _smokeParticlesInstance = null; // Освобождаем ссылку
        }

        if (_audioSource != null)
        {
            _audioSource.Stop();
        }

        Debug.Log("SmokeEffect: Stop!");
    }

    private IEnumerator SmokeEffectCoroutine()
    {
        // Создаем инстанс системы частиц, если он еще не создан
        if (_smokeParticlesInstance == null && _smokeParticles != null)
        {
            _smokeParticlesInstance = Instantiate(_smokeParticles);
        }

        // Проверяем, чтобы частицы были корректно созданы
        if (_smokeParticlesInstance != null)
        {
            _smokeParticlesInstance.Play();
            var shape = _smokeParticlesInstance.shape;
            shape.rotation = new Vector3(-90f, 0f, 0f); // Направляем выброс вверх
        }
        else
        {
            Debug.LogError("SmokeEffect: система частиц не задана!");
        }

        // Проверяем, чтобы звук был настроен
        if (_audioSource != null && _smokeSound != null)
        {
            _audioSource.clip = _smokeSound;
            _audioSource.loop = true; // Устанавливаем звук на повтор
            _audioSource.Play();
        }
        else
        {
            Debug.LogError("SmokeEffect: звук не задан!");
        }

        // Пока корутина активна, следим за положением системы частиц
        while (true)
        {
            UpdateParticlePosition();
            yield return null;
        }
    }

    private void UpdateParticlePosition()
    {
        if (_mainCamera == null || _smokeParticlesInstance == null) return;

        // Располагаем систему частиц перед камерой
        Vector3 cameraPosition = _mainCamera.transform.position;
        Vector3 cameraForward = _mainCamera.transform.forward;

        // Смещаем систему частиц немного вперед
        Vector3 smokePosition = cameraPosition + cameraForward * 2f; // 2f - расстояние перед камерой
        smokePosition.y += _offsetY;

        // Обновляем позицию системы частиц
        _smokeParticlesInstance.transform.position = smokePosition;

        // Опционально: настраиваем поворот системы частиц, если нужно (например, чтобы частицы всегда смотрели в ту же сторону, что камера)
        _smokeParticlesInstance.transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}
