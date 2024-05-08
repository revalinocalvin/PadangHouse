using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject objectToSpawn; 
    public int maxInteractions = 5; 
    private int interactionCount = 0; 

    public GameObject SpawnObject()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 1f) // Interaction radius
        {
            if (interactionCount < maxInteractions)
            {
                GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
                interactionCount++; 
                if (interactionCount >= maxInteractions)
                {
                    Destroy(gameObject); 
                }

                return spawnedObject;
            }
            else
            {
                Debug.Log("Object spawner has reached its maximum usage.");
                return null; 
            }
        }

        else {
            return null;
        }
    }
}