using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;

    private float customerNextSpawnTime;

    private int customerPerDay = 100;
    private int maxCustomerInside;
    private int customerGroupSpawn;

    void Start()
    {
        maxCustomerInside = CustomerManager.Instance.chairPoint.Length;
        customerNextSpawnTime = Time.time + 1f;
    }

    void Update()
    {
        if (customerPerDay > 0 && CustomerManager.Instance.customersInside.Length < maxCustomerInside && CustomerManager.Instance.canSpawn)
        {
            SpawnCustomer();
        }
        else
        {
            float randomNumber = Random.Range(DayTransition.Instance.interval1, DayTransition.Instance.interval2);
            customerNextSpawnTime = Time.time + randomNumber;
        }
    }

    void SpawnCustomer()
    {
        if (customerPrefab != null && Time.time >= customerNextSpawnTime)
        {
            float randomNumber = Random.Range(DayTransition.Instance.interval1, DayTransition.Instance.interval2);
            customerNextSpawnTime = Time.time + randomNumber;

            customerGroupSpawn = Random.Range(2, 5);

            customerPerDay--;
            CustomerManager.Instance.numberOfCustomers += customerGroupSpawn;

            GameManager.Instance.minStars = (CustomerManager.Instance.numberOfCustomers * 3) / 2;
            GameManager.Instance.maxStars = (CustomerManager.Instance.numberOfCustomers * 3);

            StartCoroutine(SpawnCustomers());
        }
    }

    IEnumerator SpawnCustomers()
    {
        for (int i = 0; i < customerGroupSpawn; i++)
        {
            Instantiate(customerPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
