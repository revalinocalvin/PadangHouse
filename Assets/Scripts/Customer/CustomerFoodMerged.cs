using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CustomerFoodMerged : MonoBehaviour
{
    CustomerPathing customerPathing;
    Customer customer;

    public Transform customerFoodPoint;

    private GameObject player; // Reference to the player GameObject
    private PlayerInteract playerInteract;
    private string requiredObjectTag;
    public bool order = false;
    public bool receivedFood = false;
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

    public void TakeOrder() //Change to Random Food Tag
    {
        order = true;
        requiredObjectTag = FoodList.Instance.GetRandomFood();
        Debug.Log(requiredObjectTag);
    }

    public void TrySubmitItem()
    {
        // Determine if the player is close enough to the NPC to interact
        if (Vector3.Distance(player.transform.position, transform.position) < 1.5f) // Interaction radius
        {
            // Check if the player is holding an interactable item
            foreach (Transform child in player.transform)
            {
                foreach (Transform grandchild in child)
                {
                    if (grandchild.gameObject.CompareTag(requiredObjectTag))
                    {
                        SubmitItem(grandchild.gameObject); // Submit the item to the MenuPlace
                        return;
                    }
                }
            }
        }
    }

    private void SubmitItem(GameObject food)
    {
        food.transform.SetParent(customerFoodPoint);
        food.transform.localPosition = Vector2.zero;
        playerInteract.grabbedObject = null;
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
        customerPathing.eatingFinished = true;
        CustomerManager.Instance.chairAvailable[customerPathing.chairNumber - 1] = true;
        Destroy(food);
        GameManager.Instance.AddStars(customer.customerStarsAmount);
    }
}
