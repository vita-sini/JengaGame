using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public string blockTag = "Block"; // Тег, который используется для всех блоков

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, если объект с тегом "Block" пересекает триггер
        if (other.CompareTag(blockTag))
        {
                GameEvents.InvokeGameOver();
        }
    }
}
