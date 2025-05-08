using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public string blockTag = "Block"; // Тег, который используется для всех блоков
    private HashSet<GameObject> _baseBlocks = new HashSet<GameObject>(); // Блоки, которые считаются основанием

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, если объект имеет тег "Block" и касается стола
        if (collision.gameObject.CompareTag(blockTag))
        {
            // Добавляем блок в список основания
            _baseBlocks.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Если блок перестает касаться стола, удаляем его из основания
        if (collision.gameObject.CompareTag(blockTag))
        {
            _baseBlocks.Remove(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, если объект с тегом "Block" пересекает триггер
        if (other.CompareTag(blockTag))
        {
            // Если блок не является частью основания, завершаем игру
            if (!_baseBlocks.Contains(other.gameObject))
            {
                GameEvents.InvokeGameOver();
            }
        }
    }
}
