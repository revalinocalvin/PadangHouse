using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlace : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public string interactKey = "e"; // The key used to interact
    public string requiredObjectTag = "FoodSpawn";

    void start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Debug.Log("Interact key pressed.");
            TrySubmitItem();
        }
    }

    private void TrySubmitItem()
    {
        // Determine if the player is close enough to the NPC to interact
        if (Vector3.Distance(player.transform.position, transform.position) < 1.5f) // Interaction radius
        {
            Debug.Log("Player is close enough to interact with Object.");

            // Check if the player is holding an interactable item
            foreach (Transform child in player.transform)
            {
                Debug.Log("Founded");
                if (child.gameObject.CompareTag(requiredObjectTag))
                {
                    Debug.Log("Interactable item found. Submitting item to Shelf.");
                    SubmitItem(child.gameObject); // Submit the item to the Shelf
                    return;
                }
            }
        }
    }

    private void SubmitItem(GameObject item)
    {
         Debug.Log("Submitting item to Menu Shelf.");
        // Set the position of the submitted item to the MenuPlace's position
        item.transform.position = transform.position;
        // Detach the submitted item from the player
        item.transform.SetParent(null);
    }
}