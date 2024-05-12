using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSubmission : MonoBehaviour
{
    private string interactKey = "e";

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TrySubmitItem();
        }
    }

    private void TrySubmitItem()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Food"))
            {
                SubmitItem(collider.gameObject);
            }
        }
    }

    private void SubmitItem(GameObject item)
    {
        Debug.Log("Submitting item to NPC.");
        Destroy(item);
    }
}
