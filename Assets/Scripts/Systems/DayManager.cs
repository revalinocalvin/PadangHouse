using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    private static DayManager instance;
    public static DayManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DayManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(DayManager).Name;
                    instance = obj.AddComponent<DayManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    public SaveSystem SS;

    public static Action OnDayChanged;
    public GameObject resultUI;
    public GameObject loseUI;
    public static int day { get; set; } = 1;
    public int dayValue;
    private bool hasDayChanged = false;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            SetReference();
        }
    }

    private void SetReference()
    {
        UIManager uiManager = UIManager.Instance;
        if (uiManager != null)
        {
            SS = GameObject.Find("Save System").GetComponent<SaveSystem>();
            resultUI = uiManager.resultUI;
            loseUI = uiManager.loseUI;
        }
    }

    public void SetDay()
    {
        day = dayValue;
    }

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
                if (!hasDayChanged)
                {
                    DayChanged();
                    OnDayChanged?.Invoke();
                    hasDayChanged = true; // Set the flag to true after execution
                }
            }
        }
        else if (TimeManager.Hour >= 24)
        {
            CustomerManager.Instance.canSpawn = false;
            if (!hasDayChanged)
            {
                DayChanged();
                OnDayChanged?.Invoke();
                hasDayChanged = true; // Set the flag to true after execution
            }
        }
    }

    private void DayChanged()
    {
        if (GameManager.Instance.CheckGameResults())
        {
            resultUI.SetActive(true);
            dayValue = day + 1;
            SS.SaveToJson();
        }
        else
        {
            loseUI.SetActive(true);
        }
    }
}
