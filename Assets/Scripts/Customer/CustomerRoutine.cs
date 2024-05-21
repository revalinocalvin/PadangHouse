using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CustomerRoutine : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    private PlayerInteract playerInteract;
    public string requiredObjectTag = "Food";
    public bool order = false;
    private int pathCounter = 0; // Added this to avoid compile errors. Adjust as needed.
    Vector3 direction;

    void Start()
    {
        playerInteract = player.GetComponent<PlayerInteract>();
    }

    public void TakeOrder() //Change to Random Food Tag
    {
        Debug.Log("Customer Ordered A");
        order = true;
    }

    public void TrySubmitItem()
    {
        // Determine if the player is close enough to the NPC to interact
        if (Vector3.Distance(player.transform.position, transform.position) < 1.5f) // Interaction radius
        {
            Debug.Log("Player is close enough to interact with NPC.");

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
        Debug.Log("Submitting item to NPC.");

        item.transform.SetParent(this.transform);
        item.transform.localPosition = Vector2.zero;
        playerInteract.grabbedObject = null;

        StartCoroutine(DelayedMove(item));

    }

    private IEnumerator DelayedMove(GameObject item)
    {
        yield return new WaitForSeconds(5f);
        Destroy(item);
        yield return Move();
    }

    private IEnumerator Move()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.instance.exitPoint[0].transform.position - transform.position).normalized;
        }

        while (Vector2.Distance(Customer.instance.exitPoint[0].transform.position, transform.position) > 0.1f)
        {
            transform.position += direction * Customer.instance.customerMoveSpeed * Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Movement finished
        // onTable = false; // Uncomment if necessary
        this.enabled = false;
    }
}
