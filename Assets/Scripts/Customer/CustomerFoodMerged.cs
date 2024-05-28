using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFoodMerged : MonoBehaviour
{
    CustomerPathing customerPathing;
    Customer customer;

    public Transform customerFoodPoint;

    private GameObject player;
    public GameObject food;
    private PlayerInteract playerInteract;

    private string requiredObjectTag;

    public bool order = false;
    public bool receivedFood = false;
    public bool ready = false;
    Vector3 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInteract = player.GetComponent<PlayerInteract>();
        customerPathing = this.GetComponent<CustomerPathing>();
        customer = this.GetComponent<Customer>();    
    }

    void Update()
    {
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

    public void TakeOrder()
    {
        order = true;
        requiredObjectTag = FoodList.Instance.GetRandomFood();
        Debug.Log(requiredObjectTag);
    }

    public void TrySubmitItem()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 1.5f)
        {
            foreach (Transform child in player.transform)
            {
                foreach (Transform grandchild in child)
                {
                    if (grandchild.gameObject.CompareTag(requiredObjectTag))
                    {
                        SubmitItem(grandchild.gameObject);
                        return;
                    }
                }
            }
        }
    }

    public void SubmitItem(GameObject food)
    {
        if (customerPathing.chairNumber == 9 || customerPathing.chairNumber == 10 || customerPathing.chairNumber == 11)
        {
            EatingFinished(food);
        }

        else
        {
            if (food.CompareTag(requiredObjectTag) && receivedFood == false)
            {
                this.food = food;
                food.transform.SetParent(customerFoodPoint);
                food.transform.localPosition = Vector2.zero;
                playerInteract.grabbedObject = null;
                receivedFood = true;                
            }                        
        }          
    }

    public IEnumerator WaitEatingTime()
    {
        float eatingTime = 5f;

        yield return new WaitForSeconds(eatingTime);

        EatingFinished(food);
    }

    private void EatingFinished(GameObject food)
    {
        customerPathing.eatingFinished = true;

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

        Destroy(food);
        GameManager.Instance.AddStars(customer.customerStarsAmount);
    }
}
