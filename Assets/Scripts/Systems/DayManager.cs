using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static Action OnDayChanged;
    public GameObject resultUI;
    public GameObject loseUI;
    public static int day { get; set; } = 1;
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
        if (TimeManager.Hour == 8)
        {
            CustomerManager.Instance.canSpawn = true;
        }

        if (TimeManager.Hour >= 20 && TimeManager.Hour < 24)
        {
            CustomerManager.Instance.canSpawn = false;

            if (CustomerManager.Instance.customersInside.Length == 0)
            {
                CustomerManager.Instance.canSpawn = false;
                DayChanged();
                OnDayChanged?.Invoke();
            }
        }
        else if (TimeManager.Hour >= 24)
        {
            CustomerManager.Instance.canSpawn = false;
            DayChanged();
            OnDayChanged?.Invoke();
        }
    }

    private void DayChanged()
    {
        if (GameManager.Instance.CheckGameResults())
        {
            resultUI.SetActive(true);
        }
        else
        {
            loseUI.SetActive(true);
        }
    }
}
