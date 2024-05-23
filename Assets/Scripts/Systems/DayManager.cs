using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{

    public static Action OnDayChanged;
    public static int day { get; private set; } = 1;
    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    private void TimeCheck()
    {  
        if (TimeManager.Hour == 20)
            DayChanged();
            OnDayChanged?.Invoke();
    }

    private void DayChanged()
    {
        day++;
        Debug.Log("Day Changed, DAY " + day);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene); 
    }
}
