using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;
    public GameObject rp;

    [SerializeField]
    private float rayDistance;

    public GameObject grabbedObject;
    private bool interact;
    // Start is called before the first frame update
    void Start()
    {
        grabPoint = GameObject.Find("Player/GrabPosition").transform;
        rayPoint = GameObject.Find("Player/RayPosition").transform;
        rp = GameObject.Find("Player/RayPosition");

        Collider2D collider = rp.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Change to the interaction key you prefer
        {
            Interact();
            interact = true;
            Debug.Log("Interact key pressed." + interact);
        }

         if (Input.GetKeyUp(KeyCode.E))
         {
            interact = false;
             Debug.Log("Interact key released." + interact);
         }
    }

    void Interact()
    {
        if (grabbedObject !=null)
        {
            Debug.Log("Full");
        }

        else
        {
            Debug.Log("Not Full");
        }
    }

    void OnTriggerStay2D(Collider2D collidedObject)
    {
        // Check if the colliding object is in the Pickupable layer
        if (collidedObject.gameObject.layer == LayerMask.NameToLayer("Pickupable"))
        {
            Debug.Log("RayPosition collided with a Pickupable object: " + collidedObject.gameObject.name);

            // Add your custom logic here, e.g., pick up the object
             if (interact == true)
            {
                Grab(collidedObject);
                Debug.Log("Collided!");
            }
        }
    }
    
    void Grab(Collider2D collidedObject)
    {
        if (grabbedObject == null)
            {
                grabbedObject = collidedObject.gameObject;
                grabbedObject.transform.SetParent(grabPoint); // Attach the object to the player
                grabbedObject.transform.localPosition = Vector3.zero; // Center the object on the player
                grabbedObject.GetComponent<Collider2D>().enabled = false; // Disable the object's collider
            }
    }
}
