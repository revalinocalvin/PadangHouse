using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField]
    private Transform grabPoint;

    [SerializeField]
    private Transform rayPoint;

    [SerializeField]
    private float rayDistance;

    private GameObject grabbedObject;
    private int layerIndex;
    // Start is called before the first frame update
    void Start()
    {
        layerIndex = LayerMask.NameToLayer("Pickupable");
        grabPoint = GameObject.Find("Player/GrabPosition").transform;
        rayPoint = GameObject.Find("Player/RayPosition").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Change to the interaction key you prefer
        {
            Interact();
        }
    }

    void Interact()
    {
        Grab();
    }

    void Grab()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance);

        if (hitInfo.collider!=null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            //grab object
            if (grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.transform.SetParent(grabPoint); // Attach the object to the player
                grabbedObject.transform.localPosition = Vector3.zero; // Center the object on the player
                grabbedObject.GetComponent<Collider2D>().enabled = false; // Disable the object's collider
            }
        }

        Debug.DrawRay(rayPoint.position, transform.right * rayDistance);
    }
}
