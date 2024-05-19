using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;

    private float customerNextSpawnTime;

    private int customerPerDay = 10;
    private int maxCustomerInside = 8;

    void Start()
    {
        customerNextSpawnTime = Time.time + 1f;
        GameManager.Instance.minStars = (customerPerDay * 3) / 2;
        GameManager.Instance.maxStars = (customerPerDay * 3);
    }

    void Update()
    {
        if (customerPerDay > 0 && CustomerManager.Instance.customersInside.Length < maxCustomerInside)
        {
            SpawnCustomer();
        }
        else
        {
            customerNextSpawnTime = Time.time + 3f;
        }
    }

    void SpawnCustomer()
    {
        if (customerPrefab != null && Time.time >= customerNextSpawnTime)
        {
            customerNextSpawnTime = Time.time + 3f;
            customerPerDay--;

            Instantiate(customerPrefab, transform.position, Quaternion.identity);
        }
    }
}
