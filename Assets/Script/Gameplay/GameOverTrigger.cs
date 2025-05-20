using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public string blockTag = "Block"; // ���, ������� ������������ ��� ���� ������

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���� ������ � ����� "Block" ���������� �������
        if (other.CompareTag(blockTag))
        {
                GameEvents.InvokeGameOver();
        }
    }
}
