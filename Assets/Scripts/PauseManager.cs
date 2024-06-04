using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;

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

    private void OpenPauseMenu()
    {
        pauseCanvas.SetActive(true);
    }

    private void ClosePause()
    {
        pauseCanvas.SetActive(false);
    }
}
