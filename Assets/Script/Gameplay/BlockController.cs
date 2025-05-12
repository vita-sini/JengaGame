using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private BlockSpawner _blockSpawner;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��� � ������������ ����
        {
            _blockSpawner.ReleaseBlock();
        }
    }
}
