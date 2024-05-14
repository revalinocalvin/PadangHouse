using UnityEngine;

public class RayPositionCollider : MonoBehaviour
{
    void Start()
    {
        // Check if this object has a Collider2D component
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("Collider2D component is missing on RayPosition!");
        }

        // Ensure the Collider2D is set as a trigger
        if (collider != null && !collider.isTrigger)
        {
            collider.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is in the Pickupable layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Pickupable"))
        {
            Debug.Log("RayPosition collided with a Pickupable object: " + other.gameObject.name);

            // Add your custom logic here, e.g., pick up the object
        }
    }
}