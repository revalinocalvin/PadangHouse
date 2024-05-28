using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    public Transform grabPoint;

    public GameObject grabbedObject;
    private GameObject objectToGrab;

    private HashSet<Collider2D> _objectsInTrigger = new HashSet<Collider2D>();
    private ObjectSpawner objectSpawner;
    private CustomerFoodMerged customerRoutine;
    private CustomerPathing customerPathing;
    private Table table;

    bool InArea = false; //prevent grab function error log
    private bool foodReady = false;
    private bool trashbin = false;
    private bool inTable = false;

    void Start()
    {
        grabPoint = GameObject.Find("Player/GrabPosition").transform;
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
        Collider2D[] tempObjs = new Collider2D[_objectsInTrigger.Count];
        _objectsInTrigger.CopyTo(tempObjs);
        if (tempObjs.Any(obj => obj.gameObject.CompareTag("Customer")))
        {
            customerPathing = FindClosest(_objectsInTrigger).GetComponent<CustomerPathing>();
            customerRoutine = FindClosest(_objectsInTrigger).GetComponent<CustomerFoodMerged>();
            if (customerPathing.onChair == true)
            {
                if (customerRoutine.order == true)
                {
                    Debug.Log("trying submit item");
                    customerRoutine.TrySubmitItem();
                }

                else
                {
                    customerRoutine.TakeOrder();
                }
            }
        }

        if (inTable)
        {
            table = FindClosest(_objectsInTrigger).GetComponent<Table>();
            table.Interact();
            Debug.Log("check inTable");
        }


        if (grabbedObject == null)
        {
            Debug.Log("Not Full");

            // Grabbing object method
            if (InArea)
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
            if (trashbin)
            {
                Destroy(grabbedObject);
                grabbedObject = null;
            }

            if (inTable)
            {
                table = FindClosest(_objectsInTrigger).GetComponent<Table>();

                foreach (GameObject customer in table.customers)
                {
                    Debug.Log("Customer seen?");
                    CustomerFoodMerged customerFood = customer.GetComponent<CustomerFoodMerged>();
                    if (customerFood.receivedFood == false)
                    {
                        Debug.Log("trying submit");
                        customerFood.SubmitItem(grabbedObject);
                        
                        if (customerFood.receivedFood)
                        {
                            table.Eat();
                            break;
                        }
                    }                                     
                }
            }
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

        if (collidedObject.CompareTag("FoodTray"))
        {
            Debug.Log("RayPosition collided with a Menu Dish object: " + collidedObject.gameObject.name);

            InArea = true;
            _objectsInTrigger.Add(collidedObject);
        }

        if (collidedObject.CompareTag("FoodSpawn"))
        {
            Debug.Log("RayPosition collided with a Food Spawn object: " + collidedObject.gameObject.name);

            InArea = true;
            _objectsInTrigger.Add(collidedObject);
            foodReady = true;
        }

        if (collidedObject.CompareTag("Customer"))
        {
            Debug.Log("RayPosition collided with a Customer object: " + collidedObject.gameObject.name);
            _objectsInTrigger.Add(collidedObject);
        }

        if (collidedObject.CompareTag("Trashbin"))
        {
            Debug.Log("RayPosition collided with a Trashbin object: " + collidedObject.gameObject.name);

            InArea = false;
            trashbin = true;
        }

        if (collidedObject.CompareTag("Table"))
        {
            Debug.Log("RayPosition collided with a Table object: " + collidedObject.gameObject.name);
            _objectsInTrigger.Add(collidedObject);
            inTable = true;
        }

    }

    void OnTriggerExit2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("FoodTray"))
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
            _objectsInTrigger.Remove(collidedObject);
        }

        if (collidedObject.CompareTag("Trashbin"))
        {
            Debug.Log("RayPosition not collided with a Trashbin object");
            InArea = false;
            objectToGrab = null;
            trashbin = false;
            _objectsInTrigger.Remove(collidedObject);
        }

        if (collidedObject.CompareTag("Table"))
        {
            Debug.Log("RayPosition not collided with a Table object");
            _objectsInTrigger.Remove(collidedObject);
            inTable = false;
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