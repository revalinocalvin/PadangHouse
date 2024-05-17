using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnFoodTray;
    public GameObject spawnFoodSpawn;
    public GameObject spawnFoodObject; 

    public int maxInteractions = 5; 
    private int interactionCount = 0;

    public GameObject SpawnFoodTray()
    {
        GameObject spawnedObject = Instantiate(spawnFoodTray, transform.position, Quaternion.identity);

        return spawnedObject;
    }

    public GameObject SpawnFoodSpawn()
    {
        GameObject spawnedObject = Instantiate(spawnFoodSpawn, transform.position, Quaternion.identity);

        return spawnedObject;
    }

    public GameObject SpawnFoodObject()
    {
        GameObject spawnedObject = Instantiate(spawnFoodObject, transform.position, Quaternion.identity);

        interactionCount++;

        if (interactionCount >= maxInteractions)
        {
            Destroy(gameObject);
        }

        return spawnedObject;
    }
}