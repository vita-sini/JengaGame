using UnityEngine;

public class BlockState : MonoBehaviour
{
    public enum State
    {
        Base,       // —тартовые блоки Ч нельз€ брать
        Spawning,   // “олько что заспавненный Ч можно брать
        Placed      // ”становленный Ч нельз€ брать
    }

    public State CurrentState = State.Base;

    public void SetSpawning() => CurrentState = State.Spawning;
    public void SetPlaced() => CurrentState = State.Placed;
}
