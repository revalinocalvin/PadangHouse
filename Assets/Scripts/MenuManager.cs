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
        AudioManager.Instance.DoAudio("click");
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void onContinuePress()
    {
        AudioManager.Instance.DoAudio("click");
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void onOptionPress()
    {
        AudioManager.Instance.DoAudio("click");
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void onExitPress()
    {
        AudioManager.Instance.DoAudio("click");
        Application.Quit();
        EventSystem.current.SetSelectedGameObject(null);
    }
    #endregion

    #region Button function
    #endregion
}

