using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action OnTurnEnd;
    public static event Action OnGameOver;

    public static void InvokeTurnEnd()
    {
        OnTurnEnd?.Invoke();
    }

    public static void InvokeGameOver()
    {
        OnGameOver?.Invoke();
    }
}
