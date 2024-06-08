using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public GameObject tutorial2;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            if (tutorial2.activeSelf)
            {
                tutorial2.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(false);
                SceneManager.LoadScene("Game");
            }
            Input.ResetInputAxes();
        }
    }
}
