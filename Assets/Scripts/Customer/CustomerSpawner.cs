using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;

    private float customerNextSpawnTime;

    private int maxCustomerInside;
    private int customerGroupSpawn;

    void Start()
    {
        maxCustomerInside = CustomerManager.Instance.chairPoint.Length + CustomerManager.Instance.chairPoint2.Length;
        customerNextSpawnTime = Time.time + 1f;
    }

    void Update()
    {
        if (CustomerManager.Instance.customersInside.Length < maxCustomerInside && CustomerManager.Instance.canSpawn && (CustomerManager.Instance.tableAvailable.Contains(true) || CustomerManager.Instance.chairAvailable3.Contains(true)))
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
            Debug.Log("Spawning Customers");
            float randomNumber = Random.Range(DayTransition.Instance.interval1, DayTransition.Instance.interval2);
            customerNextSpawnTime = Time.time + randomNumber;

            Loop:
            int dineInOrTakeAway = Random.Range(1, 3);

            if (dineInOrTakeAway == 1 && CustomerManager.Instance.tableAvailable.Contains(true))
            {                
                customerGroupSpawn = Random.Range(4, 5);

                if (CustomerManager.Instance.tableAvailable[0])
                {
                    StartCoroutine(SpawnCustomers(1));
                    CustomerManager.Instance.tableAvailable[0] = false;
                    CustomerManager.Instance.numberOfCustomers += customerGroupSpawn;
                }
                else if (CustomerManager.Instance.tableAvailable[1])
                {
                    StartCoroutine(SpawnCustomers(2));
                    CustomerManager.Instance.tableAvailable[1] = false;
                    CustomerManager.Instance.numberOfCustomers += customerGroupSpawn;
                }
            }

            else
            {
                if (CustomerManager.Instance.chairAvailable3.Contains(true))
                {
                    Instantiate(customerPrefab, transform.position, Quaternion.identity).GetComponent<CustomerPathing>().table = 0;
                    CustomerManager.Instance.numberOfCustomers++;
                }

                else
                {
                    goto Loop;
                }
            }

            GameManager.Instance.minStars = (CustomerManager.Instance.numberOfCustomers * 3) / 2;
            GameManager.Instance.maxStars = (CustomerManager.Instance.numberOfCustomers * 3);
        }
    }

    IEnumerator SpawnCustomers(int tableNumber)
    {
        for (int i = 0; i < customerGroupSpawn; i++)
        {
            Instantiate(customerPrefab, transform.position, Quaternion.identity).GetComponent<CustomerPathing>().table = tableNumber;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
