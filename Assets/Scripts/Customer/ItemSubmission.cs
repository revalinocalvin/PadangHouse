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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Food") && customerPathing.onChair == true)
            {
                FoodReceived(collider.gameObject);
            }
        }
    }

    private void FoodReceived(GameObject food)
    {
        receivedFood = true;
        EatingTimer(food);
    }

    private void EatingTimer(GameObject food)
    {
        /*if (customer finish eating after delay)
        {
            EatingFinished(food);
        }*/
    }

    private void EatingFinished(GameObject food)
    {
        if (receivedFood)
        {
            customerPathing.eatingFinished = true;
            Customer.Instance.chairAvailable[customerPathing.chairNumber - 1] = true;
            Destroy(food);
        }
    }
}
