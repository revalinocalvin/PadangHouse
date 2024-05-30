using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;

    private float customerNextSpawnTime;

    private int maxCustomerInside;
    private int customerGroupSpawn;

    public Table table1;
    public Table table2;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;

    public RuntimeAnimatorController anime1;
    public RuntimeAnimatorController anime2;
    public RuntimeAnimatorController anime3;
    public RuntimeAnimatorController anime4;
    public RuntimeAnimatorController anime5;

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
            //Debug.Log("Spawning Customers");
            float randomNumber = Random.Range(DayTransition.Instance.interval1, DayTransition.Instance.interval2);
            customerNextSpawnTime = Time.time + randomNumber;

        Loop:
            int dineInOrTakeAway = Random.Range(1, 11);
            Debug.Log(dineInOrTakeAway);

            if (dineInOrTakeAway <= 7 && CustomerManager.Instance.tableAvailable.Contains(true))
            {                
                customerGroupSpawn = Random.Range(2, 5);

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
                    GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
                    customer.GetComponent<CustomerPathing>().table = 0;

                    SpriteRenderer asset = customer.GetComponent<SpriteRenderer>();

                    Animator animate = customer.GetComponent<Animator>();
                    int spriteNumber = Random.Range(0, 5);
                    switch (spriteNumber)
                    {
                        case 0:
                            asset.sprite = sprite1;
                            animate.runtimeAnimatorController = anime1;
                            break;
                        case 1:
                            asset.sprite = sprite2;
                            animate.runtimeAnimatorController = anime2;
                            break;
                        case 2:
                            asset.sprite = sprite3;
                            animate.runtimeAnimatorController = anime3;
                            break;
                        case 3:
                            asset.sprite = sprite4;
                            animate.runtimeAnimatorController = anime4;
                            break;
                        case 4:
                            asset.sprite = sprite5;
                            animate.runtimeAnimatorController = anime5;
                            break;
                        default:
                            break;
                    }

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
            GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
            customer.tag = "CustomerGroup";

            SpriteRenderer asset = customer.GetComponent<SpriteRenderer>();

            Animator animate = customer.GetComponent<Animator>();
            int spriteNumber = Random.Range(0, 5);           
            switch (spriteNumber)
            {
                case 0:
                    asset.sprite = sprite1;
                    animate.runtimeAnimatorController = anime1;
                    break;
                case 1:
                    asset.sprite = sprite2;
                    animate.runtimeAnimatorController = anime2;
                    break;
                case 2:
                    asset.sprite = sprite3;
                    animate.runtimeAnimatorController = anime3;
                    break;
                case 3:
                    asset.sprite = sprite4;
                    animate.runtimeAnimatorController = anime4;
                    break;
                case 4:
                    asset.sprite = sprite5;
                    animate.runtimeAnimatorController = anime5;
                    break;
                default:
                    break;
            }
            
            customer.GetComponent<CustomerPathing>().table = tableNumber;
            if (tableNumber == 1)
            {
                table1.AddCustomers(customer);
                customer.GetComponent<Customer>().table = table1;
            }
            else
            {
                table2.AddCustomers(customer);
                customer.GetComponent<Customer>().table = table2;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
