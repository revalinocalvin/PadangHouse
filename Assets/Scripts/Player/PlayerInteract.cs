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
    public bool CustomerInteract = false;
    // bool InArea = false;
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
            Debug.Log("Interact key pressed.");
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("Interact key released.");
        }
    }

    void Interact()
    {
        if (CustomerInteract == true)
        {
            customerOrder.Order(objectToGrab);
        }

        // Grabbing object method
        if (grabbedObject == null)
        {
            Debug.Log("Not Full");

            //if (InArea == true)
            //{
                if (foodReady)
                {
                    objectToGrab = objectSpawner.SpawnObject();
                }
                Grab(objectToGrab);
            //}
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
            //InArea = true;
            objectToGrab = collidedObject.gameObject;

        }

        if (collidedObject.CompareTag("FoodSpawn"))
        {
            Debug.Log("RayPosition collided with a Menu Dish object: " + collidedObject.gameObject.name);
            //InArea = true;
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

        }
    }

    void OnTriggerExit2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Menu Dish"))
        {
            Debug.Log("RayPosition not collided with a Menu Dish object");
            //InArea = false;
            objectToGrab = null;
        }

        if (collidedObject.CompareTag("FoodSpawn"))
        {
            Debug.Log("RayPosition not collided with a Menu Dish object");
            //InArea = false;
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
}
