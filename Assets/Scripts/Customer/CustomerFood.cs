using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFood : MonoBehaviour
{
    CustomerPathing customerPathing;
    Customer customer;

    public Transform customerFoodPoint;

    public bool orderReceived = false;
    public bool receivedFood = false;

    void Start()
    {
        customerPathing = this.GetComponent<CustomerPathing>();
        customer = this.GetComponent<Customer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && orderReceived)
        {
            ReceivingFood();
        }

        if (customerPathing.onChair == true)
        {
            FoodDirection();
        }
    }

    void FoodDirection()
    {
        if (customerPathing.chairNumber == 1 || customerPathing.chairNumber == 2 || customerPathing.chairNumber == 5 || customerPathing.chairNumber == 6)
        {
            customerFoodPoint.transform.localPosition = new Vector3(0, -2, 0);
        }
        else
        {
            customerFoodPoint.transform.localPosition = new Vector3(0, 2, 0);
        }
    }

    private void ReceivingFood()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D col in cols)
        {
            if (col.gameObject.CompareTag("Food") && customerPathing.onChair == true)
            {
                FoodReceived(col.gameObject);
            }
        }
    }

    private void FoodReceived(GameObject food)
    {
        receivedFood = true;
        StartCoroutine(WaitEatingTime(food));
    }

    private IEnumerator WaitEatingTime(GameObject food)
    {
        float eatingTime = 5f;

        yield return new WaitForSeconds(eatingTime);

        EatingFinished(food);
    }

    private void EatingFinished(GameObject food)
    {
        if (receivedFood)
        {
            customerPathing.eatingFinished = true;
            CustomerManager.Instance.chairAvailable[customerPathing.chairNumber - 1] = true;
            Destroy(food);
            GameManager.Instance.AddStars(customer.customerStarsAmount);
        }
    }
}
