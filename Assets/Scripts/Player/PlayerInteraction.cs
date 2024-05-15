using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform holdPoint;

    private GameObject heldObject;
    private GameObject[] foodSpawners;

    private bool holdingObject = false;

    private void Start()
    {
        foodSpawners = GameObject.FindGameObjectsWithTag("FoodSpawn");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !holdingObject)
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.E) && holdingObject)
        {
            ServeFood();
        }

        FoodOnPlayer();
    }

    void FoodOnPlayer()
    {
        if (heldObject != null)
        {
            heldObject.transform.position = holdPoint.position;
        }
    }

    void CheckHoldPoint()
    {
        if (holdPoint.childCount == 1)
        {
            holdingObject = true;
        }
        else if (holdPoint.childCount == 0)
        {
            holdingObject = false;
        }
    }

    private void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("FoodSpawn"))
            {
                for (int i = 0; i < foodSpawners.Length; i++)
                {
                    if (collider.gameObject == foodSpawners[i])
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
        if (heldObject != null)
        {
            Collider2D col = heldObject.GetComponent<Collider2D>();

            if (col.CompareTag("Customer"))
            {
                Destroy(heldObject.gameObject);
            }
        }
    }
}