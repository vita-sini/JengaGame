using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour, IEffect
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private ParticleSystem _explosiveParticlesPrefab;
    [SerializeField] private AudioClip _explosiveSound;

    private Transform _particleSpawnPoint;
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
            _audioSource.Stop();
    }

    private IEnumerator ExplodeBlock()
    {
        yield return new WaitForSeconds(2f);

        if (_explosiveBlock != null)
        {
            Collider[] colliders = Physics.OverlapSphere(_explosiveBlock.transform.position, _explosionRadius);

            foreach (var nearbyCollider in colliders)
            {
                Rigidbody nearbyRb = nearbyCollider.GetComponent<Rigidbody>();

                if (nearbyRb != null)
                    nearbyRb.AddExplosionForce(_explosionForce, _explosiveBlock.transform.position, _explosionRadius);
            }

            if (_explosiveParticlesPrefab != null)
            {
                Vector3 spawnPosition = _particleSpawnPoint != null ? _particleSpawnPoint.position : _explosiveBlock.transform.position;
                Instantiate(_explosiveParticlesPrefab, spawnPosition, Quaternion.identity);
            }

            if (_audioSource != null && _explosiveSound != null)
                _audioSource.PlayOneShot(_explosiveSound);

            Destroy(_explosiveBlock.gameObject);
        }
    }
}
