using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotatingPlatform : MonoBehaviour, IEffect
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _target; // —юда можно вставить объект, вокруг которого вращаетс€ камера
    [SerializeField] private AudioClip _rotatingPlatformSound;

    private Coroutine _rotationEffectCoroutine;
    private AudioSource _audioSource;

    private void Awake()
    {
        GameEvents.OnTurnEnd += Stop;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        GameEvents.OnTurnEnd -= Stop;
    }

    public void Execute()
    {
        if (_rotationEffectCoroutine != null)
        {
            StopCoroutine(_rotationEffectCoroutine);
        }

        _rotationEffectCoroutine = StartCoroutine(RotationEffectCoroutine());
    }

    public void Stop()
    {
        if (_rotationEffectCoroutine != null)
        {
            StopCoroutine(_rotationEffectCoroutine);
            _rotationEffectCoroutine = null;
        }

        if (_audioSource != null)
        {
            _audioSource.Stop();
        }

        Debug.Log("RotatingPlatformEffect stopped");
    }

    private IEnumerator RotationEffectCoroutine()
    {
        Debug.Log("RotatingPlatformEffect started");

        if (_audioSource != null && _rotatingPlatformSound != null)
        {
            _audioSource.clip = _rotatingPlatformSound;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        while (true)
        {
            Camera.main.transform.RotateAround(_target.position, Vector3.up, _rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
