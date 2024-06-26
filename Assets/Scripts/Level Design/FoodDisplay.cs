using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDisplay : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public string interactKey = "e"; // The key used to interact
    public string requiredObjectTag = "FoodTray";
    private PlayerInteract playerInteract;

    void Start()
    {
        playerInteract = player.GetComponent<PlayerInteract>(); // Initialize the PlayerInteract reference

        if (playerInteract == null)
        {
            Debug.LogError("PlayerInteract component not found on the player object.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TrySubmitItem();
        }
    }

    private void TrySubmitItem()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 1f)
        {
            foreach (Transform child in player.transform)
            {
                foreach (Transform grandchild in child)
                {
                    if (grandchild.gameObject.CompareTag(requiredObjectTag) && transform.childCount == 0)
                    {
                        SubmitItem(grandchild.gameObject);
                        return;
                    }
                }
            }
        }
    }

    private void SubmitItem(GameObject item)
    {
        item.transform.position = transform.position; // Set the position of the submitted item to the MenuPlace's position

        item.transform.SetParent(this.gameObject.transform); // Detach the submitted item from the player
        item.transform.localScale = Vector3.one;
        SpriteRenderer layerItem = item.GetComponent<SpriteRenderer>();
        layerItem.sortingOrder = 2;
        item.tag = "FoodSpawn"; // Optionally change the tag or enable the collider as needed
        item.GetComponent<Collider2D>().enabled = true;
    }
}