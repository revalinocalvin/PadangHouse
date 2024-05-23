using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    private Transform grabPoint;
    private Transform rayPoint;

    public GameObject rp;
    public GameObject grabbedObject;
    private GameObject objectToGrab;
    private HashSet<Collider2D> _objectsInTrigger = new HashSet<Collider2D>();
    private ObjectSpawner objectSpawner;
    private CustomerRoutine customerRoutine;
    
    public bool CustomerInteract = false;
    bool InArea = false; //prevent grab function error log
    private bool foodReady = false;

    void Start()
    {
        grabPoint = GameObject.Find("Player/GrabPosition").transform;
        rayPoint = GameObject.Find("Player/RayPosition").transform;
        rp = GameObject.Find("Player/RayPosition");
        Collider2D collider = rp.GetComponent<Collider2D>();
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
            CheckGrabPointChild();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (Collider2D collider in _objectsInTrigger)
            {
                Debug.Log("Current GameObject" + collider.name);
            }    
        }
    }
    void CheckGrabPointChild()
    {
        if (grabPoint.childCount == 0)
        {
            grabbedObject = null;
        }
    }

    void Interact()
    {
        if (CustomerInteract == true)
        {
            if (customerRoutine.order == true)
            {
                customerRoutine.TrySubmitItem();
            }

            else
            {
                customerRoutine.TakeOrder();
            }
        }

        // Grabbing object method
        if (grabbedObject == null)
        {
            Debug.Log("Not Full");

            if (InArea == true)
            {
                if (_objectsInTrigger.Count != 0)
                {
                    objectToGrab = FindClosest(_objectsInTrigger).gameObject;
                }
                if (foodReady)
                {
                    objectSpawner = FindClosest(_objectsInTrigger).GetComponent<ObjectSpawner>();
                    objectToGrab = objectSpawner.SpawnObject();
                }
                Grab(objectToGrab);
            }
        }

        else
        {
            Debug.Log("Full");
        }
    }

    Collider2D FindClosest(HashSet<Collider2D> cols)
    {
        Collider2D[] objs = new Collider2D[cols.Count];
        cols.CopyTo(objs);
        int length = objs.Length;
        int index = 0;
        float[] distance = new float[length];
        for (int i = 0; i < length; i++)
        {
            distance[i] = Vector3.Distance(objs[i].transform.position, transform.position);
            if (distance[i] < distance[index])
                index = i;
        }
        return objs[index];
    }

    void OnTriggerEnter2D(Collider2D collidedObject)
    {

        if (collidedObject.CompareTag("Menu Dish"))
        {
            Debug.Log("RayPosition collided with a Menu Dish object: " + collidedObject.gameObject.name);

            InArea = true;
            //objectToGrab = collidedObject.gameObject;
            _objectsInTrigger.Add(collidedObject);
        }

        if (collidedObject.CompareTag("FoodSpawn"))
        {
            Debug.Log("RayPosition collided with a Food Spawn object: " + collidedObject.gameObject.name);

            InArea = true;
            _objectsInTrigger.Add(collidedObject);
            /*objectSpawner = collidedObject.GetComponent<ObjectSpawner>();*/
            foodReady = true;
        }

        if (collidedObject.CompareTag("Customer"))
        {
            Debug.Log("RayPosition collided with a Customer object: " + collidedObject.gameObject.name);
            customerRoutine = collidedObject.GetComponent<CustomerRoutine>();
            CustomerInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("Menu Dish"))
        {
            Debug.Log("RayPosition not collided with a Menu Dish object");
            InArea = false;
            objectToGrab = null;
            _objectsInTrigger.Remove(collidedObject);
        }

        if (collidedObject.CompareTag("FoodSpawn"))
        {
            Debug.Log("RayPosition not collided with a Food Spawn object");
            InArea = false;
            _objectsInTrigger.Remove(collidedObject);
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
