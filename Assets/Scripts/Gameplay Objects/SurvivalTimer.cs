using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SurvivalTimer : MonoBehaviour
{
    public static SurvivalTimer instance;

    public static event Action OnTimerFinish;

    public Text timerText;
    [HideInInspector]
    public float timeLeft = 1;
    [HideInInspector]
    public bool timerStarted = false;
    public float startDelay = 5f;

    private bool lastFrameBeforeZero = true;
    private Color originalColor;
    private AudioManager audioManager;

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
        originalColor = timerText.color;
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay - 3);
        for (int i = 0; i < 3; i++)
        {
            audioManager.StartPlaying("Timer Warning");
            yield return new WaitForSeconds(1f);
            yield return null;
        }
        audioManager.StartPlaying("Timer Start");
        StartTimer(120);
    }

    public void StartTimer(float startingTime)
    {
        timeLeft = startingTime;
        lastFrameBeforeZero = true;
        timerStarted = true;
    }

    public void AddTime(int additionalSeconds)
    {
        timeLeft += additionalSeconds;
    }

    private int lastSecond;
    private void Update()
    {
        if (timerStarted)
        {
            if (timeLeft < 15)
            {
                if ((int)timeLeft < lastSecond)
                {
                    AudioManager.instance.StartPlaying("Timer Warning");
                }
                timerText.color = Color.red;
                lastSecond = (int)timeLeft;
            }
            else
            {
                timerText.color = originalColor;
            }
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
    }

    string format = "00";
    public string ConvertToMinutesAndSeconds(float time)
    {
        return ((int)(time / 60)).ToString(format) + ":" + ((int)(time % 60)).ToString(format);
    }
}
