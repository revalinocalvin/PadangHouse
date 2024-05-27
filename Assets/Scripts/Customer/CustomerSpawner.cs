using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        maxCustomerInside = CustomerManager.Instance.chairPoint.Length + CustomerManager.Instance.chairPoint2.Length;
        customerNextSpawnTime = Time.time + 1f;
    }

    void Update()
    {
        if (customerPerDay > 0 && CustomerManager.Instance.customersInside.Length < maxCustomerInside && CustomerManager.Instance.canSpawn && CustomerManager.Instance.tableAvailable.Contains(true))
        {
            Debug.Log("Customer spawner update spawn");
            SpawnCustomer();
            
        }
        else
        {
            Debug.Log("Customer spawner update spawn else");
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

            customerGroupSpawn = Random.Range(4, 5);

            Debug.Log("Customer spawner update spawncustomer inside if");

            customerPerDay--;
            CustomerManager.Instance.numberOfCustomers += customerGroupSpawn;

            GameManager.Instance.minStars = (CustomerManager.Instance.numberOfCustomers * 3) / 2;
            GameManager.Instance.maxStars = (CustomerManager.Instance.numberOfCustomers * 3);

            if (CustomerManager.Instance.tableAvailable[0])
            {
                StartCoroutine(SpawnCustomers(1));
                CustomerManager.Instance.tableAvailable[0] = false;
            }
            else if (CustomerManager.Instance.tableAvailable[1])
            {
                StartCoroutine(SpawnCustomers(2));
                CustomerManager.Instance.tableAvailable[1] = false; 
            }
            
        }
    }

    IEnumerator SpawnCustomers(int table)
    {
        for (int i = 0; i < customerGroupSpawn; i++)
        {
            Instantiate(customerPrefab, transform.position, Quaternion.identity).GetComponent<CustomerPathing>().table = table;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
