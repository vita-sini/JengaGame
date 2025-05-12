using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private int _poolSize = 64;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject block = Instantiate(_blockPrefab);
            block.SetActive(false);
            _pool.Enqueue(block);
        }
    }

    public GameObject GetBlock()
    {
        if (_pool.Count == 0)
        {
            Debug.LogWarning("Pool is empty!");
            return null;
        }

        GameObject block = _pool.Dequeue();
        block.SetActive(true);
        return block;
    }

    public void ReturnBlock(GameObject block)
    {
        block.SetActive(false);
        _pool.Enqueue(block);
    }
}
