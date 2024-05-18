using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractV2 : MonoBehaviour
{

    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;
    public GameObject rp;

    [SerializeField]
    private float rayDistance;

    public GameObject grabbedObject;

    private ObjectSpawner objectSpawner;
    private CustomerOrder customerOrder;
    private CustomerExit customerExit;
    private GameObject objectToGrab;

    private bool interact;
    public bool isInteractTaken = false;
    private bool MenuDish = false;
    void Start()
    {
        grabPoint = GameObject.Find("Player/GrabPosition").transform;
        rayPoint = GameObject.Find("Player/RayPosition").transform;
        rp = GameObject.Find("Player/RayPosition");

        Collider2D collider = rp.GetComponent<Collider2D>();
        objectSpawner = FindObjectOfType<ObjectSpawner>();
        customerOrder = FindObjectOfType<CustomerOrder>();
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
            
            if(MenuDish = true)
            {
                Grab(objectToGrab);
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
            MenuDish = true;
            objectToGrab = collidedObject.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Menu Dish"))
        {
             Debug.Log("RayPosition not collided with a Menu Dish object");
            MenuDish = false;
        }
    }

    // void OnTriggerStay2D(Collider2D collidedObject)
    // {
    //     if (collidedObject.CompareTag("Menu Dish") && interact && isInteractTaken == false)
    //     {
    //         Debug.Log("RayPosition collided with a Menu Dish object: " + collidedObject.gameObject.name);
    //         isInteractTaken = true;            
    //         Grab(collidedObject.gameObject);

    //         Debug.Log("Collided!");
    //     }

    //     if (collidedObject.CompareTag("FoodSpawn") && interact && isInteractTaken == false)
    //     {
    //         Debug.Log("RayPosition collided with a Pickupable object: " + collidedObject.gameObject.name);
    //             GameObject spawnedObject = objectSpawner.SpawnObject(); // Attempt to spawn a new object
    //                 if (spawnedObject != null)
    //                 {  
    //                     isInteractTaken = true;
    //                     Grab(spawnedObject); // Pick up the spawned object immediately if it's not null
    //                     Debug.Log("Collided!");
    //                 }
    //     }

    //     if (collidedObject.CompareTag("Customer") && interact && isInteractTaken == true)
    //         {
    //             ItemSubmission itemSubmission = collidedObject.GetComponent<ItemSubmission>();  // Check if the ItemSubmission script is enabled on the customer GameObject

    //             if (grabbedObject != null && itemSubmission.enabled)
    //             {
    //             Debug.Log("RayPosition collided with a Pickupable object: " + collidedObject.gameObject.name);
    //             DropObject();

    //             isInteractTaken = false;
    //             Debug.Log("Interact Taken is now = " + isInteractTaken);

    //             CustomerExit customerExit = collidedObject.GetComponent<CustomerExit>();
    //                 if (customerExit != null)
    //                 {
    //                     customerExit.MoveToExit(collidedObject.gameObject);
    //                 }

    //             }
    //         }
    //             if (interact)
    //             {
    //                 Debug.Log("RayPosition collided with a Pickupable object: " + collidedObject.gameObject.name);
    //                 customerOrder.Order(collidedObject.gameObject);
    //             } 
    // }
    
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
