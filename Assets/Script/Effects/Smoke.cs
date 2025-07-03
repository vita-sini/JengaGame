using System.Collections;
using UnityEngine;

public class Smoke : MonoBehaviour, IEffect
{
    [SerializeField] private AudioClip _smokeSound;
    [SerializeField] private ParticleSystem _smokeParticles;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _distanceFrontCamera;

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
            StopCoroutine(_smokeEffectCoroutine);

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
            _smokeParticlesInstance.Stop();
            Destroy(_smokeParticlesInstance.gameObject, 1f);
            _smokeParticlesInstance = null;
        }

        if (_audioSource != null)
            _audioSource.Stop();

    }

    private IEnumerator SmokeEffectCoroutine()
    {
        if (_smokeParticlesInstance == null && _smokeParticles != null)
            _smokeParticlesInstance = Instantiate(_smokeParticles);

        if (_smokeParticlesInstance != null)
        {
            _smokeParticlesInstance.Play();
            var shape = _smokeParticlesInstance.shape;
            shape.rotation = new Vector3(-90f, 0f, 0f);
        }

        if (_audioSource != null && _smokeSound != null)
        {
            _audioSource.clip = _smokeSound;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        while (true)
        {
            UpdateParticlePosition();
            yield return null;
        }
    }

    private void UpdateParticlePosition()
    {
        if (_mainCamera == null || _smokeParticlesInstance == null) return;

        // –асполагаем систему частиц перед камерой
        Vector3 cameraPosition = _mainCamera.transform.position;
        Vector3 cameraForward = _mainCamera.transform.forward;

        // —мещаем систему частиц немного вперед
        Vector3 smokePosition = cameraPosition + cameraForward * _distanceFrontCamera;

        smokePosition.y += _offsetY;

        _smokeParticlesInstance.transform.position = smokePosition;

        _smokeParticlesInstance.transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}
