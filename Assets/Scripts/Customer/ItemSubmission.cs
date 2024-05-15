using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSubmission : MonoBehaviour
{
    CustomerPathing customerPathing;

    public bool receivedFood = false;

    void Start()
    {
        customerPathing = this.GetComponent<CustomerPathing>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ReceivingFood();
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
            Customer.Instance.chairAvailable[customerPathing.chairNumber - 1] = true;
            Destroy(food);
        }
    }
}
