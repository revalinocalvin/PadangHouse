using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private Transform grabPoint;
    private Transform rayPoint;
    public GameObject rp;
    public GameObject grabbedObject;
    private ObjectSpawner objectSpawner;
    private CustomerOrder customerOrder;
    private CustomerExit customerExit;
    private GameObject objectToGrab;
    private bool interact;
    public bool isInteractTaken = false;
    public bool InArea = false;
    private bool CustomerInteract = false;
    private bool foodReady = false;

    void Start()
    {
        grabPoint = GameObject.Find("Player/GrabPosition").transform;
        rayPoint = GameObject.Find("Player/RayPosition").transform;
        rp = GameObject.Find("Player/RayPosition");

        Collider2D collider = rp.GetComponent<Collider2D>();
        customerExit = FindObjectOfType<CustomerExit>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
            interact = true;
            Debug.Log("Interact key pressed." + interact);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            interact = false;
            Debug.Log("Interact key released." + interact);
        }
    }

    void Interact()
    {
        Debug.Log("Interact Value " + isInteractTaken);

        if (grabbedObject == null)
        {
            Debug.Log("Not Full");

            Debug.Log("Customer Interact at Interact() = " + CustomerInteract);

            if (InArea == true)
            {
                if (foodReady)
                {
                    objectToGrab = objectSpawner.SpawnObject();
                }
                Grab(objectToGrab);
            }

            if (CustomerInteract == true)
            {
                customerOrder.Order(objectToGrab);

                if (grabbedObject != null)
                { 
                    DropObject();
                }
            }

        }

        else
        {
            Debug.Log("Full");
        }
    }

    void OnTriggerEnter2D(Collider2D collidedObject)
    {

        if (collidedObject.CompareTag("Menu Dish"))
        {
            Debug.Log("RayPosition collided with a Menu Dish object: " + collidedObject.gameObject.name);
            InArea = true;
            objectToGrab = collidedObject.gameObject;

        }

        if (collidedObject.CompareTag("FoodSpawn"))
        {
            Debug.Log("RayPosition collided with a Menu Dish object: " + collidedObject.gameObject.name);
            InArea = true;
            objectSpawner = collidedObject.GetComponent<ObjectSpawner>();
            foodReady = true;
        }

        if (collidedObject.CompareTag("Customer"))
        {
            customerExit = collidedObject.GetComponent<CustomerExit>();
            ItemSubmission itemSubmission = collidedObject.GetComponent<ItemSubmission>();  // Check if the ItemSubmission script is enabled on the customer GameObject
            itemSubmission.SetCustomerExit(customerExit);
            CustomerInteract = true;

            Debug.Log("RayPosition collided with a Pickupable object: " + collidedObject.gameObject.name);
            customerOrder = collidedObject.GetComponent<CustomerOrder>();
            objectToGrab = collidedObject.gameObject;

            /*if (grabbedObject != null && itemSubmission.enabled)
            {
                Debug.Log("RayPosition collided with a Pickupable object: " + collidedObject.gameObject.name);

                customerExit = collidedObject.GetComponent<CustomerExit>();
                *//*if (customerExit != null)
                {
                    customerExit.MoveToExit(collidedObject.gameObject);
                }*//*
            }*/

        }
    }

    void OnTriggerExit2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Menu Dish"))
        {
            Debug.Log("RayPosition not collided with a Menu Dish object");
            InArea = false;
            objectToGrab = null;
        }

        if (collidedObject.CompareTag("FoodSpawn"))
        {
            Debug.Log("RayPosition not collided with a Menu Dish object");
            InArea = false;
            objectToGrab = null;
            foodReady = false;
        }

        if (collidedObject.CompareTag("Customer"))
        {
            CustomerInteract = false;
        }
    }


    void Grab(GameObject collidedObject)
    {
        Debug.Log("Grab Called");
        if (grabbedObject == null)
        {
            grabbedObject = collidedObject.gameObject;
            grabbedObject.transform.SetParent(grabPoint); // Attach the object to the player
            grabbedObject.transform.localPosition = Vector3.zero; // Center the object on the player
            grabbedObject.GetComponent<Collider2D>().enabled = false; // Disable the object's collider
                                                                      //StartCoroutine(ResetInteractTaken()); // Delay Interact bool
        }
    }

    void DropObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.SetParent(null); // Release the object from the player
            grabbedObject.GetComponent<Collider2D>().enabled = true; // Enable the object's collider
            grabbedObject = null;
        }
    }

    IEnumerator ResetInteractTaken()
    {
        // Wait for 60 frames
        for (int i = 0; i < 60; i++)
        {
            yield return null; // Waits one frame
        }

        // After 30 frames, reset the flag
        isInteractTaken = false;
        Debug.Log("isInteractTaken reset to false after 30 frames");
    }
}
