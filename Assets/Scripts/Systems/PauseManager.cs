using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;

    [Header("Selected")]
    [SerializeField] private GameObject resumeFirst;

    private bool isPaused;
    

    private void Start()
    {
        pauseCanvas.SetActive(false);
    }

    private void Update()
    {
        if (InputManager.instance.menuOpenCloseInput)
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }        
    }

    #region pause mechanic

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        OpenPauseMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        ClosePause();
    }

    #endregion

    #region pause function
    private void OpenPauseMenu()
    {
        pauseCanvas.SetActive(true);

        EventSystem.current.SetSelectedGameObject(resumeFirst);
    }

    private void ClosePause()
    {
        pauseCanvas.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    private void QuitGame()
    {
        SceneManager.LoadScene("Menu");
        
    }
    #endregion

    #region pause button actions

    public void onResumePress()
    {
        AudioManager.Instance.DoAudio("click");
        Unpause();
    }

    public void onOptionPress()
    {
        AudioManager.Instance.DoAudio("click");
    }

    public void onQuitPress()
    {
        AudioManager.Instance.DoAudio("click");
        QuitGame();
    }

    #endregion
}
