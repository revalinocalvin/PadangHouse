using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform holdPoint;
    private GameObject heldObject;
    public Vector2 rayDirection;

    private GameObject[] foodSpawners;
    private GameObject[] foodTrays;

    private bool isHolding = false;

    private void Start()
    {
        foodSpawners = GameObject.FindGameObjectsWithTag("FoodSpawn");
        foodTrays = GameObject.FindGameObjectsWithTag("FoodTray");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isHolding)
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.E) && isHolding)
        {
            ServeFood();
            DropFoodTray();
            ThrowTrash();
        }

        ObjectOnPlayer();
        CheckHoldPointChild();
    }

    void ObjectOnPlayer()
    {
        if (heldObject != null && isHolding)
        {
            heldObject.transform.position = holdPoint.position;
        }
    }

    void CheckHoldPointChild()
    {
        if (holdPoint.childCount == 1)
        {
            isHolding = true;
        }
        else if (holdPoint.childCount == 0)
        {
            heldObject = null;
            isHolding = false;
        }
    }

    private void Interact()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(holdPoint.transform.position, 0.5f);

        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("FoodSpawn"))
            {
                foodSpawners = GameObject.FindGameObjectsWithTag("FoodSpawn");

                for (int i = 0; i < foodSpawners.Length; i++)
                {
                    if (col.gameObject == foodSpawners[i])
                    {
                        ObjectSpawner objectSpawner = foodSpawners[i].GetComponent<ObjectSpawner>();

                        heldObject = objectSpawner.SpawnFoodObject();
                        heldObject.transform.SetParent(holdPoint);
                        break;
                    }
                }
            }
            else if (col.CompareTag("FoodTray"))
            {
                for (int i = 0; i < foodTrays.Length; i++)
                {
                    if (col.gameObject == foodTrays[i])
                    {
                        ObjectSpawner objectSpawner = foodTrays[i].GetComponent<ObjectSpawner>();

                        heldObject = objectSpawner.SpawnFoodTray();
                        heldObject.transform.SetParent(holdPoint);
                        Destroy(col.gameObject);
                        break;
                    }
                }
            }
            else if (col.CompareTag("Customer"))
            {
                CustomerPathing customerPathing = col.GetComponent<CustomerPathing>();
                CustomerFood customerFood = col.GetComponent<CustomerFood>();

                if (customerFood.orderReceived == false && customerPathing.onChair == true)
                {
                    customerFood.orderReceived = true;
                }
            }
        }
    }

    private void ServeFood()
    {
        if (heldObject != null && heldObject.CompareTag("Food"))
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(heldObject.transform.position, 0.5f);

            foreach (Collider2D col in cols)
            {
                CustomerFood customerFood = col.GetComponent<CustomerFood>();
                CustomerPathing customerPathing = col.GetComponent<CustomerPathing>();

                if (col.CompareTag("Customer") && customerFood.receivedFood == false && customerPathing.onChair == true && customerFood.orderReceived == true)
                {
                    GameObject food = heldObject;
                    heldObject = null;

                    food.transform.SetParent(null);
                    food.transform.SetParent(customerFood.customerFoodPoint);
                    food.transform.localPosition = new Vector3(0f, 0f, 0f);
                }
            }
        }
    }

    private void DropFoodTray()
    {
        if (heldObject != null && heldObject.CompareTag("FoodTray"))
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(heldObject.transform.position, 0.5f);

            foreach (Collider2D col in cols)
            {
                FoodDisplay foodDisplay = col.GetComponent<FoodDisplay>();

                if (col.CompareTag("FoodDisplay") && foodDisplay.foodOnDisplay == false)
                {
                    Destroy(heldObject);
                    heldObject = null;

                    ObjectSpawner objectSpawner = col.GetComponent<ObjectSpawner>();

                    GameObject foodSpawn = objectSpawner.SpawnFoodSpawn();
                    foodSpawn.transform.SetParent(col.gameObject.transform);
                    foodSpawn.transform.localPosition = new Vector3(0, 0, 0);
                    break;
                }
            }
        }
    }

    private void ThrowTrash()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(holdPoint.transform.position, 0.5f);

        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("Trashbin") && heldObject != null)
            {
                Destroy(heldObject);
                heldObject = null;
            }
        }
    }
}