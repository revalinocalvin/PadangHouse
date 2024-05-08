using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionV2 : MonoBehaviour
{
    private GameObject heldObject;
    

    private void Start()
    {
        
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
            if (collider.CompareTag("Menu Dish")) // Check if the player is interacting with the object spawner
            {
                PickUpObject(collider.gameObject);
                return;
            }
            else if (collider.CompareTag("Menu Place"))
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