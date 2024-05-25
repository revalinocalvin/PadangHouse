using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;

    private float customerNextSpawnTime;

    private int customerPerDay = 100;
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
        /*else
        {
            float randomNumber = Random.Range(4.0f, 11.0f);
            customerNextSpawnTime = Time.time + randomNumber;
        }*/
    }

    void SpawnCustomer()
    {
        if (customerPrefab != null && Time.time >= customerNextSpawnTime)
        {
            float randomNumber = Random.Range(DayTransition.Instance.interval1, DayTransition.Instance.interval2);
            customerNextSpawnTime = Time.time + randomNumber;
            customerPerDay--;

            Debug.Log("Interval 1 = " + DayTransition.Instance.interval1);

            Instantiate(customerPrefab, transform.position, Quaternion.identity);
        }
    }
}
