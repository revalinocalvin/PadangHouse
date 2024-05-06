using UnityEngine;

public class ItemSubmission : MonoBehaviour
{
    public NPCMovement npc; // Reference to the NPC's movement script
    public Vector2 exitPosition; // Define this in the inspector, where the NPC should head to leave
    public GameObject player; // Reference to the player GameObject
    public string interactKey = "e"; // The key used to interact
    public string requiredObjectTag = "Interactable";

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
            Debug.Log("Player is close enough to interact with NPC.");

            // Check if the player is holding an interactable item
            foreach (Transform child in player.transform)
            {
                if (child.gameObject.CompareTag(requiredObjectTag))
                {
                    Debug.Log("Interactable item found. Submitting item to NPC.");
                    SubmitItem(child.gameObject); // Submit the item to the NPC
                    return;
                }
            }
        }
    }

    private void SubmitItem(GameObject item)
    {
        Debug.Log("Submitting item to NPC.");
        
        // Disable or destroy the interactable item
        item.SetActive(false); // Hide the item instead of destroying it

        // Make the NPC leave after item submission
        npc.ExitScreen(exitPosition);

        npc.ReenableMovementAfterSubmission();
    }
}
