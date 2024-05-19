using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(GameManager).Name;
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public TMP_Text starsText;

    public GameObject gameResultBackground;
    public TMP_Text gameResultText;

    private int currentStars;
    public int minStars;
    public int maxStars;

    private float winConDelay;

    void Start()
    {
        Time.timeScale = 1;
        currentStars = 0;
        winConDelay = Time.time + 15f;
    }

    void Update()
    {
        UpdateStarsText();

        if (CustomerManager.Instance.customersInside.Length == 0 && Time.time >= winConDelay)
        {
            WinLose();
        }
    }

    void UpdateStarsText()
    {
        starsText.text = currentStars.ToString() + "/" + maxStars.ToString() + " Stars";
    }

    void WinLose()
    {
        if (currentStars < minStars)
        {
            gameResultBackground.SetActive(true);
            gameResultText.text = "You lose!";
            Time.timeScale = 0;
        }
        else if (currentStars >= minStars)
        {
            gameResultBackground.SetActive(true);
            gameResultText.text = "You win!";
            Time.timeScale = 0;
        }
    }

    public void AddStars(int value)
    {
        currentStars += value;
    }
}
