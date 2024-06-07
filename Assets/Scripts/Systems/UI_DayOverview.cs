using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_DayOverview : MonoBehaviour
{
    public TextMeshProUGUI _dayX;
    public TextMeshProUGUI _customerArrived;
    public TextMeshProUGUI _customerAngry;
    public TextMeshProUGUI _customerServed;
    public TextMeshProUGUI _starGained;

    [SerializeField] private GameObject advanceFirst;

    [Header("Elements")]
    public Sprite star;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(advanceFirst);
        _dayX.text = $"Day {DayManager.day} Overview";
        _customerArrived.text = $"Customer Arrived\t= {CustomerManager.Instance.numberOfCustomers}";
        _customerAngry.text = $"Customer Angry\t\t= {CustomerManager.Instance.angryCustomers}";
        _customerServed.text = $"Customer Served\t= {CustomerManager.Instance.servedCustomer}";
        _starGained.text = $"Stars Gained\t\t= {GameManager.Instance.currentStars} <sprite name=star>";        
    }

    #region Button action
    public void onAdvancePress()
    {
        nextDay();
    }

    public void onLeavePress() 
    {
        SceneManager.LoadScene("Menu");
    }
    #endregion

    #region Button function
    public void nextDay()
    {
        this.gameObject.SetActive(false);
        DayManager.day++;
        CustomerManager.Instance.numberOfCustomers = 0;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene(currentScene);        
    }
    #endregion
}
