using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    private PlayerInteract playerInteract;

    void Start()
    {
        playerInteract = player.GetComponent<PlayerInteract>();
    }

    void Update()
    {
        // Optional: Your update logic here
    }

    public void Order(GameObject customer)
    {
        Debug.Log("Customer Ordered A");
        ItemSubmission itemSubmission = customer.GetComponent<ItemSubmission>();
        if (itemSubmission != null)
        {
            itemSubmission.enabled = true;
        }
    }
}
