using System.Collections;
using System.Collections.Generic;
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

    public bool CustomerInteract = false;
    bool InArea = false; //prevent grab function error log
    private bool foodReady = false;
    private bool trashbin = false;

    void Start()
    {
        grabPoint = GameObject.Find("Player/GrabPosition").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
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
            if (trashbin)
            {
                Destroy(grabbedObject);
                grabbedObject = null;
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
            InArea = true;
            _objectsInTrigger.Add(collidedObject);
        }

        if (collidedObject.CompareTag("FoodSpawn"))
        {
            InArea = true;
            _objectsInTrigger.Add(collidedObject);
            foodReady = true;
        }

        if (collidedObject.CompareTag("Customer"))
        {
            customerRoutine = collidedObject.GetComponent<CustomerFoodMerged>(); //ganti customer food
            customerPathing = collidedObject.GetComponent<CustomerPathing>();
            CustomerInteract = true;
        }

        if (collidedObject.CompareTag("Trashbin"))
        {
            InArea = false;
            trashbin = true;
        }
    }

    void OnTriggerExit2D(Collider2D collidedObject)
    {
        if (collidedObject.CompareTag("FoodTray"))
        {
            InArea = false;
            objectToGrab = null;
            _objectsInTrigger.Remove(collidedObject);
        }

        if (collidedObject.CompareTag("FoodSpawn"))
        {
            InArea = false;
            _objectsInTrigger.Remove(collidedObject);
            objectToGrab = null;
            foodReady = false;
        }

        if (collidedObject.CompareTag("Customer"))
        {
            CustomerInteract = false;
        }

        if (collidedObject.CompareTag("Trashbin"))
        {
            InArea = false;
            objectToGrab = null;
            trashbin = false;
            _objectsInTrigger.Remove(collidedObject);
        }
    }

    void Grab(GameObject collidedObject)
    {
        if (grabbedObject == null)
        {
            grabbedObject = collidedObject.gameObject;
            grabbedObject.transform.SetParent(grabPoint); // Attach the object to the player
            grabbedObject.transform.localPosition = Vector3.zero; // Center the object on the player
            grabbedObject.GetComponent<Collider2D>().enabled = false; // Disable the object's collider
        }
    }
}
