using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    public TextMeshProUGUI dayText;

    void Start()
    {
        dayText.text = $"Day {DayManager.day}";
    }

    private void OnEnable()
    {
        DayManager.OnDayChanged += UpdateDay;
    }

    private void OnDisable()
    {
        DayManager.OnDayChanged -= UpdateDay;   
    }

    private void UpdateDay()
    {
        dayText.text = $"{DayManager.day}";
    }
}
