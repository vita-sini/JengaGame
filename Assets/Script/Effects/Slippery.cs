using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slippery : MonoBehaviour, IEffect
{
    [SerializeField] private PhysicMaterial _slipperyMaterial; 
    [SerializeField] private AudioClip _slipperySound;

    private AudioSource _audioSource;
    private PhysicMaterial _defaultMaterial; // Для хранения исходного материала
    private List<Collider> _slipperyBlocks = new List<Collider>();
    private Coroutine _slipperyEffectCoroutine;

    private void Awake()
    {
        GameEvents.OnTurnEnd += Stop;
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        GameEvents.OnTurnEnd -= Stop;
    }

    public void Execute()
    {
        if (_slipperyEffectCoroutine != null)
            StopCoroutine(_slipperyEffectCoroutine);

        _slipperyEffectCoroutine = StartCoroutine(SlipperyEffectCoroutine());
    }

    public void Stop()
    {
        if (_slipperyEffectCoroutine != null)
        {
            StopCoroutine(_slipperyEffectCoroutine);
            _slipperyEffectCoroutine = null;
        }

        if (_audioSource != null)
            _audioSource.Stop();

        ResetBlocks();
    }

    private IEnumerator SlipperyEffectCoroutine()
    {
        if (_audioSource != null && _slipperySound != null)
        {
            _audioSource.clip = _slipperySound;
            _audioSource.Play();
        }

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        if (blocks.Length > 0)
        {
            foreach (var block in blocks)
            {
                Collider collider = block.GetComponent<Collider>();

                if (collider != null)
                {
                    //Сохраняем исходный материал
                    if (_defaultMaterial == null)
                    {
                        _defaultMaterial = collider.sharedMaterial;
                    }
                    _slipperyBlocks.Add(collider);
                    collider.material = _slipperyMaterial;
                }
            }
        }

        yield return null;
    }

    private void ResetBlocks()
    {
        foreach (var collider in _slipperyBlocks)
        {
            if (collider != null)
            {
                collider.material = _defaultMaterial; // Возвращаем блоки в исходное состояние
            }
        }

        _slipperyBlocks.Clear();
    }
}
