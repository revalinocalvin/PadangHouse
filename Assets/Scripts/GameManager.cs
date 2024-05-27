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

    void Start()
    {
        Time.timeScale = 1;
        currentStars = 0;
    }

    void Update()
    {
        UpdateStarsText();
    }

    void UpdateStarsText()
    {
        starsText.text = currentStars.ToString() + " Stars";
    }

    public bool CheckGameResults()
    {
        if (currentStars >= minStars)
        {
            return true;
        }
        else
        {
            gameResultBackground.SetActive(true);
            gameResultText.text = "You lose!\n Stars needed: " + minStars;
            Time.timeScale = 0;
            return false;
        }
    }

    public void AddStars(int value)
    {
        currentStars += value;
    }
}
