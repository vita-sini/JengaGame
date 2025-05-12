using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private BlockPool _blockPool;

    public GameObject CurrentSpawnedBlock { get; private set; }

    private void Start()
    {
        SpawnBlock(); // Спавним первый блок при старте игры
    }

    public void SpawnBlock()
    {
        if (CurrentSpawnedBlock != null) return;

        GameObject block = _blockPool.GetBlock();
        if (block == null) return;

        block.transform.position = _spawnPoint.position;
        block.transform.rotation = Quaternion.identity;

        Rigidbody rb = block.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        var blockState = block.GetComponent<BlockState>();

        if (blockState != null)
        {
            blockState.SetSpawning();
        }

        CurrentSpawnedBlock = block;
    }

    public void ReleaseBlock()
    {
        if (CurrentSpawnedBlock == null) return;

        Rigidbody rb = CurrentSpawnedBlock.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        var blockState = CurrentSpawnedBlock.GetComponent<BlockState>();

        if (blockState != null)
        {
            blockState.SetPlaced();
        }

        CurrentSpawnedBlock = null;

        // ⏱️ Автоматически спавним следующий блок с задержкой
        Invoke(nameof(SpawnBlock), 1.0f); // подождать 1 сек перед спавном нового
    }
}
