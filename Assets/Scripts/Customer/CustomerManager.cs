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
    public GameObject exitPoint;

    public bool[] chairAvailable;

    public GameObject[] customersInside;

    void Start()
    {
        ChairAvailable();
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
    }

    void CheckCustomersInside()
    {
        customersInside = GameObject.FindGameObjectsWithTag("Customer");
    }
}
