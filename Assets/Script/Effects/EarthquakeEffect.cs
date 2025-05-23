using System.Collections;
using UnityEngine;

public class EarthquakeEffect : MonoBehaviour, IEffect
{
    [SerializeField] private float _earthquakeForce;
    [SerializeField] private float _effectDuration;
    [SerializeField] private AudioClip _earthquakeSound;
    [SerializeField] private float _startTime = 3f;

    private AudioSource _audioSource;
    private Coroutine _earthquakeEffectCoroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Execute()
    {
        if (_earthquakeEffectCoroutine != null)
            StopCoroutine(_earthquakeEffectCoroutine);

        _earthquakeEffectCoroutine = StartCoroutine(EarthquakeEffectCoroutine());
    }

    public void Stop()
    {
        if (_earthquakeEffectCoroutine != null)
        {
            StopCoroutine(_earthquakeEffectCoroutine);
            _earthquakeEffectCoroutine = null;
        }

        if (_audioSource != null)
            _audioSource.Stop();
    }

    private IEnumerator EarthquakeEffectCoroutine()
    {
        if (_audioSource != null && _earthquakeSound != null)
        {
            _audioSource.clip = _earthquakeSound;
            _audioSource.time = _startTime;
            _audioSource.Play();
        }

        float elapsedTime = 0f;

        Vector3 initialCameraPosition = Camera.main.transform.position;

        CamRotate camRotate = Camera.main.GetComponent<CamRotate>();

        if (camRotate) camRotate.enabled = false;

        while (elapsedTime < _effectDuration)
        {
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

            foreach (GameObject block in blocks)
            {
                if (block.TryGetComponent(out Rigidbody rb))
                {
                    Vector3 shakeDirection = new Vector3(
                        Random.Range(-1f, 1f),
                        0,
                        Random.Range(-1f, 1f)
                    ).normalized;

                    rb.AddForce(shakeDirection * _earthquakeForce, ForceMode.Impulse);
                }
            }
            // Тряска камеры
            Camera.main.transform.position = initialCameraPosition + new Vector3(
                Mathf.Sin(Time.time * 20f) * 0.1f,
                Mathf.Cos(Time.time * 20f) * 0.1f,
                0
            );

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        // Возвращение камеры в исходное положение
        if (camRotate) camRotate.enabled = true;

        Camera.main.transform.position = initialCameraPosition;

        Stop();
    }
}
