using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] bool isCountDown;
    [SerializeField] float time;

    bool isRunning = false;
    TimeSpan timespan;

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        if (isRunning)
        {
            if (isCountDown)
            {
                time -= Time.deltaTime;
            }
            else
            {
                time += Time.deltaTime;
            }

            if (isCountDown && time < 0)
            {
                time = 0;
                isRunning = false;
            }

            timespan = TimeSpan.FromSeconds(time);

            timerText.text = string.Format("{0:D2}:{1:D2}",timespan.Minutes,timespan.Seconds);
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
