using GameRoot;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameOverTrigger : MonoBehaviour
    {
        [SerializeField] private GameEvents _gameEvents;

        public string blockTag = "Block";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(blockTag))
                _gameEvents.OnInvokeGameOver();
        }
    }
}
