using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Vector2 startPosition; // Starting position, typically off-screen
    public Vector2 targetPosition; // Target position, where you want the NPC to move to
    public float speed = 5.0f; // Speed at which the NPC should move
    private Vector2 exitPosition; // Position where the NPC will move to exit
    private bool shouldExit = false; // Flag to check if NPC should start exiting

    void Start()
    {
        // Set the initial position of the NPC (off-screen)
        transform.position = startPosition;
        Debug.Log("NPC position set to starting position: " + startPosition);

        // Optionally, you can use StartCoroutine if you want a delay before starting movement
        //StartCoroutine(BeginMovementAfterDelay(1.0f)); // 1 second delay before moving
    }

    void Update()
    {
        if (!shouldExit)
        {
            // Move the NPC from start to target position smoothly
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            Debug.Log("NPC moving towards target position: " + targetPosition);

            // Optionally, you can stop moving when reaching the target
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                Debug.Log("NPC reached target position.");
                // Do something when the target is reached, e.g., stop moving or disable the script
                this.enabled = false; // Stops the Update loop
            }
        }
        else
        {
            // Move the NPC towards the exit position
            transform.position = Vector2.MoveTowards(transform.position, exitPosition, speed * Time.deltaTime);
            Debug.Log("NPC moving towards exit position: " + exitPosition);

            // Optionally, deactivate the NPC when it reaches the exit position
            if (Vector2.Distance(transform.position, exitPosition) < 0.1f)
            {
                Debug.Log("NPC reached exit position. Deactivating NPC.");
                gameObject.SetActive(false); // Or Destroy(gameObject); if you want to completely remove the NPC
            }
        }
    }

    public void ExitScreen(Vector2 newExitPosition)
    {
        exitPosition = newExitPosition;
        shouldExit = true;
        Debug.Log("NPC exit position set to: " + exitPosition);
        Debug.Log("shouldExit flag set to: " + shouldExit);
    }
    public void ReenableMovementAfterSubmission()
    {

        StartCoroutine(BeginMovementAfterDelay(0.2f));
    }

    // Example of starting movement with a delay
    IEnumerator BeginMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Movement logic can be triggered here if not in Update
         this.enabled = true;
    }
}
