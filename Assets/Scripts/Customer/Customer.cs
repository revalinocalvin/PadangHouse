using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    CustomerPathing customerPathing;
    CustomerFoodMerged customerFood;

    public int customerStarsAmount = 3;
    [SerializeField] private float customerPatience = 20f;
    private float patienceTimer;
    private bool stillPatient;
    private bool patienceTimerSet;

    public GameObject customerOrder;
    public GameObject FoodOrange;
    public GameObject FoodBlue;
    public GameObject FoodRed;
    public GameObject[] customerStars;

    void Start()
    {
        stillPatient = true;
        patienceTimer = Time.time;
        patienceTimerSet = false;

        customerPathing = GetComponent<CustomerPathing>();
        customerFood = GetComponent<CustomerFoodMerged>();
        customerOrder.SetActive(false);
    }

    void Update()
    {
        if (customerPathing.onChair == true && patienceTimerSet == false)
        {
            patienceTimerSet = true;
            patienceTimer = Time.time + customerPatience;
        }

        if (customerFood.receivedFood == false)
        {
            Patience();
        }

        OrderSign();
    }

    void OrderSign()
    {
        if (customerFood.order == false && customerPathing.onChair == true)
        {
            customerOrder.SetActive(true);
        }
        else
        {
            customerOrder.SetActive(false);
        }
    }

    public void Foodsign(string tag)
    {
        if (tag == "Food1")
        {
            FoodRed.SetActive(true) ;
        }
        else if (tag == "Food2")
        {
            FoodBlue.SetActive(true);
        }
        else if (tag == "Food3")
        {
            FoodOrange.SetActive(true);
        }
    }

    void Patience()
    {
        if (!stillPatient)
        {
            customerPathing.eatingFinished = true;

            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = Color.red;
        }
        else if (Time.time >= patienceTimer && patienceTimerSet == true && customerFood.receivedFood == false)
        {
            patienceTimer = Time.time + customerPatience;
            customerStars[customerStarsAmount - 1].SetActive(false);
            customerStars[customerStarsAmount - 1] = null;
            customerStarsAmount -= 1;

            if (customerStarsAmount == 0)
            {
                stillPatient = false;

                if (customerPathing.table == 1)
                { 
                    CustomerManager.Instance.chairAvailable[customerPathing.chairNumber - 1] = true;
                    CustomerManager.Instance.CheckTableAvailable(1);
                }
                else if (customerPathing.table == 2)
                {
                    CustomerManager.Instance.chairAvailable2[customerPathing.chairNumber - 5] = true;
                    CustomerManager.Instance.CheckTableAvailable(2);
                }
                else
                {
                    CustomerManager.Instance.chairAvailable3[customerPathing.chairNumber - 9] = true;
                }
            }
        }
    }
}
