using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform holdPoint;
    public Vector2 rayDirection;

    private GameObject heldObject;
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
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("FoodSpawn"))
            {
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
        }
    }

    private void ServeFood()
    {
        if (holdPoint != null && heldObject != null && heldObject.CompareTag("Food"))
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(heldObject.transform.position, 0.5f);

            foreach (Collider2D col in cols)
            {
                ItemSubmission colItemSubmission = col.GetComponent<ItemSubmission>();
                CustomerPathing colPathing = col.GetComponent<CustomerPathing>();

                if (col.CompareTag("Customer") && colItemSubmission.receivedFood == false && colPathing.onChair == true)
                {
                    GameObject food = heldObject;
                    heldObject = null;

                    food.transform.SetParent(null);
                    food.transform.SetParent(col.gameObject.transform);

                    //Later change to customer's holdPoint instead
                    Vector3 foodOffset = new Vector3(0f, -1f, 0f);
                    food.transform.localPosition = foodOffset;
                }
            }
        }
    }

    private void GrabFoodTray()
    {

    }
}