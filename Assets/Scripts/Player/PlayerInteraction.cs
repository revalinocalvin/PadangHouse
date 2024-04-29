using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool isCustomer = false;

    void Start()
    {
        
    }

    void Update()
    {
        Interact();
    }

    void Interact()
    {
        if (isCustomer && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Customer"))
        {
            isCustomer = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Customer"))
        {
            isCustomer = false;
        }
    }
}
