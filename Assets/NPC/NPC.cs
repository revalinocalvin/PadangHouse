using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string requiredObjectTag = "Interactable"; // Tag of the object the NPC requires

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("NPC OnTriggerEnter2D called");
        Debug.Log("Collision ada bro dengan: " + collision.gameObject.name); // Check which object the NPC is colliding with
        if (collision.CompareTag(requiredObjectTag))
        {
            // Remove the object
            Destroy(collision.gameObject);
            Debug.Log("NPC received the required object!");
            // Add any additional logic here, such as giving rewards or triggering events
        }
    }
}
