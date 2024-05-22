using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject objectToSpawn; 
    public int maxInteractions = 1; 
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
            else
            {
                Debug.Log("Object spawner has reached its maximum usage.");
                return null; 
            }
    }
}