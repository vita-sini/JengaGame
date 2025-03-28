using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : MonoBehaviour, IEffect
{
    [SerializeField] private int _numberOfHeavyBlocks; // Количество усиленных блоков
    [SerializeField] private float _heavyMass; // Увеличенная масса для усиленных блоков
    [SerializeField] private AudioClip _heavySound;

    private AudioSource _audioSource;
    private List<Rigidbody> _heavyBlocks = new List<Rigidbody>();
    private Coroutine _heavyEffectCoroutine;

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
        if (_heavyEffectCoroutine != null)
        {
            StopCoroutine(_heavyEffectCoroutine);
        }

        _heavyEffectCoroutine = StartCoroutine(HeavyEffectCoroutine());
    }

    public void Stop()
    {
        if (_heavyEffectCoroutine != null)
        {
            StopCoroutine(_heavyEffectCoroutine);
            _heavyEffectCoroutine = null;
        }

        if (_audioSource != null)
        {
            _audioSource.Stop();
        }

        ResetBlocks();
    }

    private IEnumerator HeavyEffectCoroutine()
    {
        if (_audioSource != null && _heavySound != null)
        {
            _audioSource.clip = _heavySound;
            _audioSource.Play();
        }

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        if (blocks.Length > 0)
        {
            for (int i = 0; i < _numberOfHeavyBlocks; i++)
            {
                int randomIndex = Random.Range(0, blocks.Length);
                GameObject randomBlock = blocks[randomIndex];
                Rigidbody rb = randomBlock.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    _heavyBlocks.Add(rb);
                    rb.mass = _heavyMass;
                }
            }
        }

        yield return null;
    }

    private void ResetBlocks()
    {
        foreach (var rb in _heavyBlocks)
        {
            if (rb != null)
            {
                rb.mass = 300f; // Возвращаем блоки в исходное состояние
            }
        }

        _heavyBlocks.Clear();
    }
}
