using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    

    [SerializeField] private GameObject playFirst;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(playFirst);
    }

    #region Button action
    public void onPlayPress()
    {
        SceneManager.LoadScene("Game");
        SceneManager.UnloadScene("Menu");
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void onContinuePress()
    {

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void onOptionPress()
    {

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void onExitPress()
    {

        EventSystem.current.SetSelectedGameObject(null);
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

