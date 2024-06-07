using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject playFirst;
    [SerializeField] private GameObject optionMenu;

    private SaveSystem saveSystem;

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
        if (File.Exists(SaveSystem.savePath))
        {
            SceneManager.LoadScene("Game");
            EventSystem.current.SetSelectedGameObject(null);
            saveSystem = FindObjectOfType<SaveSystem>();
            saveSystem.LoadFromJson();
        }
    }

    public void onOptionPress()
    {
        optionMenu.SetActive(true);
        this.gameObject.SetActive(false);

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

