using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System;
using UnityEngine;

public class DayTransition : MonoBehaviour
{
    public static DayTransition instance;
    public static DayTransition Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DayTransition>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(DayTransition).Name;
                    instance = obj.AddComponent<DayTransition>();
                }
            }
            return instance;
        }
    }

    CustomerSpawner customerSpawner;

    public float interval1 { get; private set; }
    public float interval2 { get; private set; }
    public float timer { get; private set; }

    private void Start()
    {
        interval1 = IntervalCalculate()[0];
        interval2 = IntervalCalculate()[1];
        timer = TimeCalculate();
        Debug.Log("Day Transition Started");
    }

    float TimeCalculate()
    {
        int day = DayManager.day;
        float baseTime = 60f;
        float X = 2f - (float)Math.Pow(0.8, day - 4.1);
        float irlTime = baseTime + baseTime * X;
        Debug.Log("Irl Day Time = " + irlTime);
        float result = irlTime / 720;
        return result;
    }

    float[] IntervalCalculate()
    {
        int day = DayManager.day;
        float baseInterval1 = 6f;
        float baseInterval2 = 10f;
        float range1 = 5f;
        float range2 = 7f;

        float X = 1f - (float)Math.Pow(0.8, day - 1f);
        float result1 = baseInterval1 - range1 * X;
        float result2 = baseInterval2 - range2 * X;
        Debug.Log("Interval1 Calculating = " + result1);
        return new float[] {result1,result2};
    }
}
