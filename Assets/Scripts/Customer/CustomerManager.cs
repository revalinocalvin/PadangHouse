using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public float customerMoveSpeed = 5f;

    public static CustomerManager instance;
    public static CustomerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CustomerManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(CustomerManager).Name;
                    instance = obj.AddComponent<CustomerManager>();
                }
            }
            return instance;
        }
    }

    public GameObject[] pathPoint;
    public GameObject[] chairPoint;
    public GameObject[] chairPoint2;
    public GameObject[] table;
    public GameObject exitPoint;

    public bool[] chairAvailable;
    public bool[] chairAvailable2;
    public bool[] tableAvailable;

    public GameObject[] customersInside;
    public int numberOfCustomers;

    public bool canSpawn = true;

    void Start()
    {
        ChairAvailable();
        TableAvailable();
        CheckCustomersInside();
    }

    void Update()
    {
        CheckCustomersInside();
    }

    private void ChairAvailable()
    {
        chairAvailable = new bool[chairPoint.Length];

        for (int i = 0; i < chairPoint.Length; i++)
        {
            chairAvailable[i] = true;
        }

        chairAvailable2 = new bool[chairPoint2.Length];

        for (int i = 0; i < chairPoint2.Length; i++)
        {
            chairAvailable2[i] = true;
        }
    }

    private void TableAvailable()
    {
        tableAvailable = new bool[table.Length];

        for (int i = 0; i < table.Length; i++)
        {
            tableAvailable[i] = true;
        }
    }

    void CheckCustomersInside()
    {
        customersInside = GameObject.FindGameObjectsWithTag("Customer");
    }
}
