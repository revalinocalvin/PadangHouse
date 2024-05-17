using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    CustomerPathing customerPathing;
    CustomerFood customerFood;

    private float customerPatience = 10f;
    private float patienceTimer;
    private bool stillPatient;
    private bool patienceTimerSet;

    public GameObject customerOrder;

    void Start()
    {
        stillPatient = true;
        patienceTimer = Time.time;
        patienceTimerSet = false;

        customerPathing = GetComponent<CustomerPathing>();
        customerFood = GetComponent<CustomerFood>();
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
        if (customerFood.orderReceived == false && customerPathing.onChair == true)
        {
            customerOrder.SetActive(true);
        }
        else
        {
            customerOrder.SetActive(false);
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
        else
        {
            if (Time.time >= patienceTimer && patienceTimerSet == true)
            {
                stillPatient = false;
                GameManager.Instance.AddSatisfaction(-10);
            }
        }
    }
}
