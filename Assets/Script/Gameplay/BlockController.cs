using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private BlockSpawner _blockSpawner;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Ћ ћ Ч активировать блок
        {
            _blockSpawner.ReleaseBlock();
        }
    }
}
