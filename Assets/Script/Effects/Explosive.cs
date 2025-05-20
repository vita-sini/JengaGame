using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour, IEffect
{
    [SerializeField] private float _explosionForce; // Сила взрыва
    [SerializeField] private float _explosionRadius; // Радиус взрыва
    [SerializeField] private ParticleSystem _explosiveParticlesPrefab; // Префаб ParticleSystem
    [SerializeField] private AudioClip _explosiveSound; // Звук взрыва
    private Transform _particleSpawnPoint; // Точка спауна системы частиц

    private Rigidbody _explosiveBlock;
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
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        // Фильтруем блоки, у которых состояние Placed
        List<Rigidbody> placedBlocks = new List<Rigidbody>();

        foreach (GameObject block in blocks)
        {
            BlockState blockState = block.GetComponent<BlockState>();
            Rigidbody rb = block.GetComponent<Rigidbody>();

            if (blockState != null && rb != null && blockState.CurrentState == BlockState.State.Placed)
            {
                placedBlocks.Add(rb);
            }
        }

        // Выбираем случайный из подходящих блоков
        if (placedBlocks.Count > 0)
        {
            int randomIndex = Random.Range(0, placedBlocks.Count);
            _explosiveBlock = placedBlocks[randomIndex];

            StartCoroutine(ExplodeBlock());
        }
    }

    public void Stop()
    {
        _explosiveBlock = null;

        if (_audioSource != null)
        {
            _audioSource.Stop();
        }
    }

    private IEnumerator ExplodeBlock()
    {
        yield return new WaitForSeconds(2f); // Задержка перед взрывом

        if (_explosiveBlock != null)
        {
            // Создаём взрывные силы
            Collider[] colliders = Physics.OverlapSphere(_explosiveBlock.transform.position, _explosionRadius);

            foreach (var nearbyCollider in colliders)
            {
                Rigidbody nearbyRb = nearbyCollider.GetComponent<Rigidbody>();
                if (nearbyRb != null)
                {
                    nearbyRb.AddExplosionForce(_explosionForce, _explosiveBlock.transform.position, _explosionRadius);
                }
            }

            // Создаём эффект частиц в месте взрыва
            if (_explosiveParticlesPrefab != null)
            {
                Vector3 spawnPosition = _particleSpawnPoint != null ? _particleSpawnPoint.position : _explosiveBlock.transform.position;
                Instantiate(_explosiveParticlesPrefab, spawnPosition, Quaternion.identity);
            }

            // Воспроизводим звук взрыва
            if (_audioSource != null && _explosiveSound != null)
            {
                _audioSource.PlayOneShot(_explosiveSound);
            }

            // Уничтожаем блок
            Destroy(_explosiveBlock.gameObject);
        }
    }
}
