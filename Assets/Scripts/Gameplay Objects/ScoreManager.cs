using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static event Action<int> OnScoreChange;

    private int currentScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddScore(int score)
    {
        currentScore += score;
        OnScoreChange?.Invoke(currentScore);
    }

    public int GetScore()
    {
        return currentScore;
    }
}
