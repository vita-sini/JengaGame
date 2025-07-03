using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, IEffect
{
    [SerializeField] private int _numberOfGhostBlocks;
    [SerializeField] private Material _ghostMaterial;
    [SerializeField] private AudioClip _ghostSound;

    private List<Renderer> _ghostBlocks = new List<Renderer>();
    private List<Material> _originalMaterials = new List<Material>();
    private Coroutine _ghostEffectCoroutine;
    private AudioSource _audioSource;

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
        if (_ghostEffectCoroutine != null)
            StopCoroutine(_ghostEffectCoroutine);

        _ghostEffectCoroutine = StartCoroutine(GhostEffectCoroutine());
    }

    public void Stop()
    {
        if (_ghostEffectCoroutine != null)
        {
            StopCoroutine(_ghostEffectCoroutine);
            _ghostEffectCoroutine = null;
        }

        if (_audioSource != null)
            _audioSource.Stop();

        ResetBlocks();
    }

    private IEnumerator GhostEffectCoroutine()
    {
        if (_audioSource != null && _ghostSound != null)
        {
            _audioSource.clip = _ghostSound;
            _audioSource.Play();
        }

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        if (blocks.Length > 0)
        {
            for (int i = 0; i < _numberOfGhostBlocks; i++)
            {
                int randomIndex = Random.Range(0, blocks.Length);
                GameObject randomBlock = blocks[randomIndex];
                Renderer renderer = randomBlock.GetComponent<Renderer>();

                if (renderer != null)
                {
                    if (!_ghostBlocks.Contains(renderer))
                    {
                        _ghostBlocks.Add(renderer);
                        _originalMaterials.Add(renderer.material);
                        renderer.material = _ghostMaterial;
                    }
                }
            }
        }

        yield return null;
    }

    private void ResetBlocks()
    {
        for (int i = 0; i < _ghostBlocks.Count; i++)
        {
            if (_ghostBlocks[i] != null && _originalMaterials[i] != null)
                _ghostBlocks[i].material = _originalMaterials[i]; // ¬озвращаем блоки в исходное состо€ние

        }

        _ghostBlocks.Clear();
        _originalMaterials.Clear();
    }
}
