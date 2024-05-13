using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInteraction : MonoBehaviour
{
    private GameObject heldObject;
    private PlayerInteractionV2 playerInteractionV2;
    //private ObjectSpawner objectSpawner;

    private void Start()
    {
        playerInteractionV2 = FindObjectOfType<PlayerInteractionV2>();
        //objectSpawner = FindObjectOfType<ObjectSpawner>(); // Find the ObjectSpawner in the scene
        //GameObject[] foodSpawns = GameObject.FindGameObjectsWithTag("Menu Dish");
        //objectSpawner = new ObjectSpawner[foodSpawns.Length];
        //for (int i = 0; i < foodSpawns.Length; i++)
        //{
        //    objectSpawner[i] = foodSpawns[i].GetComponent<ObjectSpawner>();
        //}
    }

    public void Interact(GameObject Player)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Player.transform.position, 1.0f); // Adjust the radius as needed
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Menu Dish"))
            {
                //PickUpObject(collider.gameObject);
                //playerInteractionV2.PickUpObject(gameObject);
                return;
            }

            else if (collider.CompareTag("Customer"))
            {
                Debug.Log("Masuk");
                DropObject();
            }
        }
    }

    private void PickUpObject(GameObject obj)
    {
        heldObject = obj;
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
          heldObject.transform.SetParent(playerObject.transform); // Set the parent to the player GameObject
          heldObject.transform.localPosition = Vector3.zero; // Center the object on the player
         heldObject.GetComponent<Collider2D>().enabled = false; // Disable the object's collider
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }

    public void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null); // Release the object from the player
            heldObject.GetComponent<Collider2D>().enabled = true; // Enable the object's collider
            heldObject = null;
        }
    }
}
