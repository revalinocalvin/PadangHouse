using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSubmission : MonoBehaviour
{
    private string interactKey = "e";
    private bool receivedFood = false;

    CustomerPathing customerPathing;

    void Start()
    {
        customerPathing = this.GetComponent<CustomerPathing>();
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            ReceivingFood();
        }
    }

    private void ReceivingFood()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Food") && customerPathing.onChair == true)
            {
                FoodReceived(collider.gameObject);
            }
        }
    }

    private void FoodReceived(GameObject item)
    {
        receivedFood = true;
        EatingTimer();

        Destroy(item);
    }

    private void EatingTimer()
    {
        /*if (customer finish eating after delay)
        {
            EatingFinished();
        }*/
        EatingFinished();
    }

    private void EatingFinished()
    {
        if (receivedFood)
        {
            customerPathing.eatingFinished = true;
            Customer.Instance.chairAvailable[customerPathing.chairNumber - 1] = true;
        }
    }
}
