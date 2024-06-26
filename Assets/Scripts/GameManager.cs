using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public int currentStars;
    public int totalStars;
    public int minStars;
    public int maxStars;

    void Start()
    {
        Time.timeScale = 1;
        currentStars = 0;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            UpdateStarsText();
        }
    }

    void UpdateStarsText()
    {
        starsText.text = currentStars.ToString();
    }

    public bool CheckGameResults()
    {
        if (currentStars >= minStars)
        {
            Time.timeScale = 0;
            return true;
        }
        else
        {
            Time.timeScale = 0;
            return false;
        }
    }

    public void AddStars(int value)
    {
        currentStars += value;
    }
}
