using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionHandler : MonoBehaviour
{
    public GameObject volumeSlider;
    public GameObject mainMenu;
    public Slider slider;
    private void Start()
    {
        slider = volumeSlider.GetComponent<Slider>();
    }

    private void Update()
    {        

        if (Input.GetKeyDown(KeyCode.A))
        {
            slider.value -= 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            slider.value += 0.1f;
        }


        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (mainMenu != null) 
            {
                this.gameObject.SetActive(false);
                mainMenu.SetActive(true);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(slider.value);
    }
}
