using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : MonoBehaviour, IEffect
{
    [SerializeField] private int _numberOfGlitchedBlocks; // Количество блоков для эффекта
    [SerializeField] private float _dragMultiplier; // Множитель линейного сопротивления
    [SerializeField] private float _angularDragMultiplier; // Множитель углового сопротивления
    [SerializeField] private AudioClip _glitchSound;

    private List<Rigidbody> _glitchedBlocks = new List<Rigidbody>();
    private Coroutine _glitchEffectCoroutine;
    private Dictionary<Rigidbody, float> _originalDrags = new Dictionary<Rigidbody, float>();
    private Dictionary<Rigidbody, float> _originalAngularDrags = new Dictionary<Rigidbody, float>();
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
        if (_glitchEffectCoroutine != null)
        {
            StopCoroutine(_glitchEffectCoroutine);
        }

        _glitchEffectCoroutine = StartCoroutine(GlitchEffectCoroutine());
    }

    public void Stop()
    {
        if (_glitchEffectCoroutine != null)
        {
            StopCoroutine(_glitchEffectCoroutine);
            _glitchEffectCoroutine = null;
        }

        RestoreOriginalParameters();

        _glitchedBlocks.Clear();
        _originalDrags.Clear();
        _originalAngularDrags.Clear();

        Debug.Log("GlitchEffect stopped");
    }

    private IEnumerator GlitchEffectCoroutine()
    {
        Debug.Log("GlitchEffect started");

        if (_audioSource != null && _glitchSound != null)
        {
            _audioSource.clip = _glitchSound;
            _audioSource.Play();
            _audioSource.loop = false;
        }

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        if (blocks.Length > 0)
        {
            for (int i = 0; i < _numberOfGlitchedBlocks; i++)
            {
                int randomIndex = Random.Range(0, blocks.Length);
                GameObject randomBlock = blocks[randomIndex];

                if (randomBlock.TryGetComponent(out Rigidbody rb) && !_glitchedBlocks.Contains(rb))
                {
                    // Сохраняем оригинальные параметры
                    _originalDrags[rb] = rb.drag;
                    _originalAngularDrags[rb] = rb.angularDrag;

                    // Увеличиваем сопротивление
                    rb.drag *= _dragMultiplier;
                    rb.angularDrag *= _angularDragMultiplier;

                    _glitchedBlocks.Add(rb);
                }
            }
        }

        yield return null;
    }

    private void RestoreOriginalParameters()
    {
        foreach (var rb in _glitchedBlocks)
        {
            if (rb != null)
            {
                // Восстанавливаем оригинальные параметры
                rb.drag = _originalDrags[rb];
                rb.angularDrag = _originalAngularDrags[rb];
            }
        }
    }
}
