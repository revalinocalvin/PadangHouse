using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; 
    public int maxInteractions = 5; 
    private int interactionCount = 0;

    public GameObject SpawnObject()
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

        return null;
    }
}