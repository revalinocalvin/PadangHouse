using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    CustomerPathing customerPathing;
    CustomerFood customerFood;

    public GameObject customerOrder;

    void Start()
    {
        customerPathing = GetComponent<CustomerPathing>();
        customerFood = GetComponent<CustomerFood>();
        customerOrder.SetActive(false);
    }

    void Update()
    {
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
}
