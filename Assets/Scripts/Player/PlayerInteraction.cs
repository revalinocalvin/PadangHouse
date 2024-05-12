using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject heldObject;
    private GameObject[] foodSpawners;

    private void Start()
    {
        //Get all FoodSpawn objects
        foodSpawners = GameObject.FindGameObjectsWithTag("FoodSpawn");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        FoodOnPlayer();
    }

    void FoodOnPlayer()
    {
        if (heldObject != null)
        {
            heldObject.transform.position = transform.position;
        }
    }

    private void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("FoodSpawn"))
            {
                //Reference the foodSpawners that the player interacts with if there are multiple foodSpawners
                for (int i = 0; i < foodSpawners.Length; i++)
                {
                    if (collider.gameObject == foodSpawners[i])
                    {
                        ObjectSpawner objectSpawner = foodSpawners[i].GetComponent<ObjectSpawner>();

                        heldObject = objectSpawner.SpawnObject();
                        break;
                    }
                }
            }
        }
    }
}