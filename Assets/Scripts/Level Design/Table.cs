using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    
    public HashSet<GameObject> customers = new HashSet<GameObject>();
    public float radius = 3f;

    public void AddCustomers(GameObject customer)
    {
        customers.Add(customer);
    }

    public void Interact()
    {                
        if (InteractCheck())
        {
            foreach (GameObject customer in customers) 
            {
                CustomerFoodMerged customerOrder = customer.GetComponent<CustomerFoodMerged>();
                if (customerOrder.order == false)
                {
                    customerOrder.TakeOrder();                    
                }

                Customer customerSatisfaction = customer.GetComponent<Customer>();
                //customerSatisfaction.table = this.GetComponent<Table>();
            }
        }
    }

    public bool InteractCheck()
    {
        bool allOnChair = true;

        foreach (GameObject customer in customers)
        {
            CustomerPathing customerPathing = customer.GetComponent<CustomerPathing>();
            if (!customerPathing.onChair)
            {
                allOnChair = false;
            }
        }
        return allOnChair;
    }

    public void Eat()
    {
        if (FoodCheck())
        {
            foreach (GameObject customer in customers)
            {
                CustomerFoodMerged customerOrder = customer.GetComponent<CustomerFoodMerged>();
                customerOrder.ready = true;
                customerOrder.StartCoroutine(customerOrder.WaitEatingTime());
                
                Customer icon = customer.GetComponent<Customer>();
                icon.orderIcon.SetActive(false);
            
            }
            customers.Clear();

            
        }        
    }
    public bool FoodCheck()
    {
        bool foodsPlaced = true;

        foreach (GameObject customer in customers)
        {
            CustomerFoodMerged customerOrder = customer.GetComponent<CustomerFoodMerged>();
            if (!customerOrder.receivedFood)
            {
                foodsPlaced = false;
            }
        }
        return foodsPlaced;
    }

    public void CustomerAngry(Customer exclude)
    {
        foreach (GameObject customer in customers)
        {
            Customer customerSatisfaction = customer.GetComponent<Customer>();
            if (customerSatisfaction != exclude)
            {                
                customerSatisfaction.Angry();
            }

            Customer icon = customer.GetComponent<Customer>();
            if (icon.orderIcon != null)
            {
                icon.orderIcon.SetActive(false);
            }
            
        }
        customers.Clear();
    }

    public void PatienceStart()
    {
        foreach (GameObject customer in customers)
        {
            Customer customerSatisfaction = customer.GetComponent<Customer>();
            customerSatisfaction.patienceTimer = Time.time + 5f;
            customerSatisfaction.patienceTimerSet = true;
        }
    }
}
