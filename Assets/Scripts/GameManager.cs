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

    public TMP_Text satisfactionText;
    private int satisfactionValue;

    public GameObject gameResultBackground;
    public TMP_Text gameResultText;

    [SerializeField] private int gameOverValue = -20;
    [SerializeField] private int winValue = 100;

    void Start()
    {
        Time.timeScale = 1;
        satisfactionValue = 0;
    }

    void Update()
    {
        UpdateSatisfactionText();
        WinLose();
    }

    void UpdateSatisfactionText()
    {
        satisfactionText.text = satisfactionValue.ToString() + " Satisfaction";
    }

    void WinLose()
    {
        if (satisfactionValue <= gameOverValue)
        {
            gameResultBackground.SetActive(true);
            gameResultText.text = "You lose!";
            Time.timeScale = 0;
        }
        else if (satisfactionValue >= winValue)
        {
            gameResultBackground.SetActive(true);
            gameResultText.text = "You win!";
            Time.timeScale = 0;
        }
    }

    public void AddSatisfaction(int value)
    {
        satisfactionValue += value;
    }
}
