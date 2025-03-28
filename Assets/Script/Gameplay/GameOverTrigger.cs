using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public string blockTag = "Block"; // ���, ������� ������������ ��� ���� ������
    private HashSet<GameObject> baseBlocks = new HashSet<GameObject>(); // �����, ������� ��������� ����������

    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ���� ������ ����� ��� "Block" � �������� �����
        if (collision.gameObject.CompareTag(blockTag))
        {
            // ��������� ���� � ������ ���������
            baseBlocks.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // ���� ���� ��������� �������� �����, ������� ��� �� ���������
        if (collision.gameObject.CompareTag(blockTag))
        {
            baseBlocks.Remove(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���� ������ � ����� "Block" ���������� �������
        if (other.CompareTag(blockTag))
        {
            // ���� ���� �� �������� ������ ���������, ��������� ����
            if (!baseBlocks.Contains(other.gameObject))
            {
                GameEvents.InvokeGameOver();
            }
        }
    }
}
