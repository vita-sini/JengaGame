using System.Collections;
using UnityEngine;

public class WindEffect : MonoBehaviour, IEffect
{
    [SerializeField] private float _windForce;
    [SerializeField] private float _effectDuration;
    [SerializeField] private ParticleSystem _windParticlesPrefab;
    [SerializeField] private Transform _particleSpawnPoint;

    private ParticleSystem _windParticlesInstance;
    private Coroutine _windEffectCoroutine;

    public void Execute()
    {
        if (_windEffectCoroutine != null)
            StopCoroutine(_windEffectCoroutine);

        _windEffectCoroutine = StartCoroutine(WindEffectCoroutine());
    }

    public void Stop()
    {
        if (_windEffectCoroutine != null)
        {
            StopCoroutine(_windEffectCoroutine);
            _windEffectCoroutine = null;
        }

        if (_windParticlesInstance != null)
        {
            _windParticlesInstance.Stop();
            Destroy(_windParticlesInstance.gameObject);
        }
    }

    private IEnumerator WindEffectCoroutine()
    {
        if (_windParticlesPrefab != null)
        {
            _windParticlesInstance = Instantiate(_windParticlesPrefab, _particleSpawnPoint.position, Quaternion.identity);
            _windParticlesInstance.transform.rotation = Quaternion.LookRotation(Vector3.up); // Направляем вверх
            _windParticlesInstance.Play();
        }

        float elapsedTime = 0f;

        while (elapsedTime < _effectDuration)
        {
            if (_windParticlesInstance != null)
                _windParticlesInstance.transform.Translate(Vector3.right * Time.deltaTime * 10f);

            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

            foreach (GameObject block in blocks)
            {
                if (block.TryGetComponent(out Rigidbody rb))
                {
                    // Синусоидальный ветер для более реалистичного эффекта
                    float windStrength = _windForce * Mathf.Sin(Time.time * 0.01f);
                    Vector3 windDirection = Vector3.right * windStrength;
                    rb.AddForce(windDirection, ForceMode.Force);
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (_windParticlesInstance != null)
        {
            _windParticlesInstance.Stop();
            Destroy(_windParticlesInstance.gameObject);
        }

        Stop();
    }
}
