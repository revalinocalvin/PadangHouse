using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionV2 : MonoBehaviour
{
    private GameObject heldObject;
    private ObjectSpawner objectSpawner;

    private void Start()
    {
        objectSpawner = FindObjectOfType<ObjectSpawner>(); // Find the ObjectSpawner in the scene
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Change to the interaction key you prefer
        {
            Interact();
        }
    }

    private void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f); // Adjust the radius as needed
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("FoodSpawn")) // Check if the player is interacting with the object spawner
            {
                GameObject spawnedObject = objectSpawner.SpawnObject(); // Attempt to spawn a new object
                if (spawnedObject != null)
                {
                    PickUpObject(spawnedObject); // Pick up the spawned object immediately if it's not null
                }
                break;
            }
            else if (collider.CompareTag("Customer"))
            {
                DropObject();
            }
        }
    }

    private void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        heldObject.transform.SetParent(transform); // Attach the object to the player
        heldObject.transform.localPosition = Vector3.zero; // Center the object on the player
        heldObject.GetComponent<Collider2D>().enabled = false; // Disable the object's collider
    }

    private void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null); // Release the object from the player
            heldObject.GetComponent<Collider2D>().enabled = true; // Enable the object's collider
            heldObject = null;
        }
    }
}