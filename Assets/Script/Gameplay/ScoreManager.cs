using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; }
    public event Action<int> OnScoreChanged;   // ������� ��� UI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);      // ���� �� ���� ������
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Add(int points)
    {
        CurrentScore += points;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        OnScoreChanged?.Invoke(CurrentScore);
    }
}
