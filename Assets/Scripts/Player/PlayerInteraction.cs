using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private string displayTableLayer = "DisplayTable";
    private string pickupTableLayer = "PickupTable";
    private int displayTableLayerNumber;
    private int pickupTableLayerNumber;
    private int displayTableLayerMask;
    private int pickupTableLayerMask;

    public Transform holdPoint;
    public Vector2 rayDirection;

    private GameObject heldObject;
    private GameObject[] foodSpawners;

    private bool isHolding = false;

    private void Start()
    {
        displayTableLayerNumber = LayerMask.NameToLayer(displayTableLayer);
        displayTableLayerMask = LayerMask.GetMask(displayTableLayer);

        pickupTableLayerNumber = LayerMask.NameToLayer(pickupTableLayer);
        pickupTableLayerMask = LayerMask.GetMask(pickupTableLayer);

        foodSpawners = GameObject.FindGameObjectsWithTag("FoodSpawn");
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
        CheckHoldPoint();
    }

    void ObjectOnPlayer()
    {
        if (heldObject != null && isHolding)
        {
            heldObject.transform.position = holdPoint.position;
        }
    }

    void CheckHoldPoint()
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
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, rayDirection, 1.5f, displayTableLayerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, rayDirection, 1.5f, pickupTableLayerMask);

        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("FoodSpawn") && hit1.collider.gameObject.layer == displayTableLayerNumber)
            {
                for (int i = 0; i < foodSpawners.Length; i++)
                {
                    if (col.gameObject == foodSpawners[i])
                    {
                        ObjectSpawner objectSpawner = foodSpawners[i].GetComponent<ObjectSpawner>();

                        heldObject = objectSpawner.SpawnObject();
                        heldObject.transform.SetParent(holdPoint);
                        break;
                    }
                }
            }
        }
    }

    private void ServeFood()
    {
        if (holdPoint != null && heldObject != null)
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
}