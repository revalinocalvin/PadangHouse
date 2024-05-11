using UnityEngine;

public class ItemSubmission : MonoBehaviour
{
    //public NPCMovement npc; // Reference to the NPC's movement script
    //public Vector2 exitPosition; // Define this in the inspector, where the NPC should head to leave

    public GameObject player; // Reference to the player GameObject
    public string interactKey = "e"; // The key used to interact
    [SerializeField] private string requiredObjectTag = "Food";

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                foreach (Transform heldObj in player.transform)
                {
                    Debug.Log("Founded");
                    if (heldObj.gameObject.CompareTag(requiredObjectTag))
                    {
                        Debug.Log("Interactable item found. Submitting item to NPC.");
                        SubmitItem(heldObj.gameObject); // Submit the item to the NPC
                        return;
                    }
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
        //npc.ExitScreen(exitPosition);

        //npc.ReenableMovementAfterSubmission();
    }
}
