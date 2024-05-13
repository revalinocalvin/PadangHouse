using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionV3 : MonoBehaviour
{
    private GameObject heldObject;
    private MenuInteractionV2 menuInteractionV2;

    private void Start()
    {
        menuInteractionV2 = FindObjectOfType<MenuInteractionV2>();
        if (menuInteractionV2 == null)
    {
        Debug.LogError("MenuInteraction component not found in the scene.");
    }
    else
    {
        Debug.Log("MenuInteraction found: " + menuInteractionV2.name);
    }
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
        if (menuInteractionV2 != null)
        {
            menuInteractionV2.Interact(gameObject);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f); // Adjust the radius as needed
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Customer"))
            {
                Debug.Log("Masuk");
                DropObject();
            }
        }
    }

    public void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        heldObject.transform.SetParent(transform); // Attach the object to the player
        heldObject.transform.localPosition = Vector3.zero; // Center the object on the player
        heldObject.GetComponent<Collider2D>().enabled = false; // Disable the object's collider
    }

    public void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null); // Release the object from the player
            heldObject.GetComponent<Collider2D>().enabled = true; // Enable the object's collider
            heldObject = null;
        }
    }
}
