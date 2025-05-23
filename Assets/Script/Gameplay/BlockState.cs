using UnityEngine;

public class BlockState : MonoBehaviour
{
    public enum State
    {
        Base,       // ��������� ����� � ������ �����
        Spawning,   // ������ ��� ������������ � ����� �����
        Placed      // ������������� � ������ �����
    }

    public State CurrentState = State.Base;

    public void SetSpawning() => CurrentState = State.Spawning;
    public void SetPlaced() => CurrentState = State.Placed;
}
