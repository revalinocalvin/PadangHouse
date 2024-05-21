using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlace : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public string interactKey = "e"; // The key used to interact
    public string requiredObjectTag = "Menu Dish";
    private PlayerInteract playerInteract;

    void Start()
    {

        playerInteract = player.GetComponent<PlayerInteract>(); // Initialize the PlayerInteract reference

        if (playerInteract == null)
        {
            Debug.LogError("PlayerInteract component not found on the player object.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TrySubmitItem();
        }
    }

    private void TrySubmitItem()
    {
        // Determine if the player is close enough to the MenuPlace to interact
        if (Vector3.Distance(player.transform.position, transform.position) < 1f) // Interaction radius
        {
            Debug.Log("Player is close enough to interact with MenuPlace.");

            // Check if the player is holding an interactable item
            foreach (Transform child in player.transform)
            {
                foreach (Transform grandchild in child)
                {
                    if (grandchild.gameObject.CompareTag(requiredObjectTag))
                    {
                        Debug.Log("Interactable item found. Submitting item to MenuPlace.");
                        SubmitItem(grandchild.gameObject); // Submit the item to the MenuPlace
                        return;
                    }
                }
            }
        }
    }

    private void SubmitItem(GameObject item)
    {
        Debug.Log("Submitting item to Menu Shelf.");
        item.transform.position = transform.position; // Set the position of the submitted item to the MenuPlace's position
        
        item.transform.SetParent(null); // Detach the submitted item from the player
       
        item.tag = "FoodSpawn"; // Optionally change the tag or enable the collider as needed
        item.GetComponent<PolygonCollider2D>().enabled = true;

        enabled = false; // Optionally disable the script if interaction is one-time

        // Set the grabbedObject property of PlayerInteract to null
        if (playerInteract != null)
        {
            playerInteract.grabbedObject = null;
            Debug.Log("PlayerInteract reference is now null.");
        }
        else
        {
            Debug.LogError("PlayerInteract reference is null.");
        }
    }
}