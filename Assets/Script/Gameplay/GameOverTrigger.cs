using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTrigger : MonoBehaviour
{
    public string blockTag = "Block";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(blockTag))
            GameEvents.InvokeGameOver();
    }
}
