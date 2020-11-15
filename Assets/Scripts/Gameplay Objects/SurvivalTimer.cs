using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SurvivalTimer : MonoBehaviour
{
    public static SurvivalTimer instance;

    public static event Action OnTimerFinish;

    public TextMeshProUGUI timerText;
    [HideInInspector]
    public float timeLeft;

    private bool lastFrameBeforeZero = true;

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

    private void Start()
    {
        StartTimer(130);
    }

    public void StartTimer(float startingTime)
    {
        timeLeft = startingTime;
        lastFrameBeforeZero = true;
    }

    public void AddTime(int additionalSeconds)
    {
        timeLeft += additionalSeconds;
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = ConvertToMinutesAndSeconds(timeLeft);
        }
        else if (lastFrameBeforeZero)
        {
            OnTimerFinish?.Invoke();
            timeLeft = 0;
            lastFrameBeforeZero = false;
        }
    }

    string format = "00";
    public string ConvertToMinutesAndSeconds(float time)
    {
        return ((int)(time / 60)).ToString(format) + ":" + ((int)(time % 60)).ToString(format);
    }
}
