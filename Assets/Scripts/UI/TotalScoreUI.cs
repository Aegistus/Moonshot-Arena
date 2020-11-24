using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScoreUI : MonoBehaviour
{
    public Text scoreText;

    private void Awake()
    {
        ScoreManager.OnScoreChange += UpdateScoreText;
    }

    private void UpdateScoreText(int obj)
    {
        scoreText.text = "Score: " + obj;
    }
}
