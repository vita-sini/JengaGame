using UnityEngine;

namespace Gameplay
{
    public class BlockState : MonoBehaviour
    {
        private BlockRegistry _blockRegistry;

        public State CurrentState = State.Base;

        private void OnDisable()
        {
            _blockRegistry?.Unregister(gameObject);
        }

        public void Initialize(BlockRegistry registry)
        {
            _blockRegistry = registry;
        }

        public void SetSpawning() => CurrentState = State.Spawning;

        public void SetPlaced()
        {
            CurrentState = State.Placed;
            _blockRegistry?.Register(gameObject);
        }

        public enum State
        {
            Base,
            Spawning,
            Placed
        }
    }
}
