using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public TextMeshProUGUI dayText;

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
        dayText.text = $"Day {DayManager.day}";
    }
}
