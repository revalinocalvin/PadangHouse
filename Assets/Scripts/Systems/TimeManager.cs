using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute {  get; private set; }
    public static int Hour { get; private set; }

    //private float minuteToRealTime = DayTransition.Instance.timer;
    private float timer;

    void Start()
    {
        Minute = 0;
        Hour = 8;
        timer = DayTransition.Instance.timer;
        Debug.Log("Timer = " + timer);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        OnMinuteChanged?.Invoke();

        if (timer <= 0)
        {
            Minute++;
            if(Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
            }

            if (Hour >= 20)
            {
                timer = 1f;
            }
            else
            {
                timer = DayTransition.Instance.timer;
            }
            
        }
    }
}
