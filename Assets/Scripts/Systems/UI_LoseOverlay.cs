using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_LoseOverlay : MonoBehaviour
{
    public TextMeshProUGUI _dayX;
    public TextMeshProUGUI _customerArrived;
    public TextMeshProUGUI _customerAngry;
    public TextMeshProUGUI _customerServed;
    public TextMeshProUGUI _starGained;
    public TextMeshProUGUI _starMinimum;
    public TextMeshProUGUI _starTotal;

    public GameObject restartFirst;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(restartFirst);
        _dayX.text = $"Day {DayManager.day} Overview";
        _customerArrived.text = $"Customer Arrived\t= {CustomerManager.Instance.numberOfCustomers}";
        _customerAngry.text = $"Customer Angry\t\t= {CustomerManager.Instance.angryCustomers}";
        _customerServed.text = $"Customer Served\t= {CustomerManager.Instance.servedCustomer}";
        _starGained.text = $"Stars Gained\t\t= {GameManager.Instance.currentStars} <sprite name=star>";
        _starMinimum.text = $"Stars Minimum\t\t= {GameManager.Instance.minStars} <sprite name=star>";
        _starTotal.text = $"Stars Total\t\t= {GameManager.Instance.totalStars} <sprite name=star>";
    }

    #region Button action
    public void onRestartPress()
    {
        Restart();
    }

    public void onLeavePress()
    {
        SceneManager.LoadScene("Menu");
    }
    #endregion

    #region Button function
    public void Restart()
    {
        this.gameObject.SetActive(false);
        DayManager.day = 1;
        GameManager.Instance.totalStars = 0;
        CustomerManager.Instance.numberOfCustomers = 0;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene(currentScene);
    }
    #endregion
}
