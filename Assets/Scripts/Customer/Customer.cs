using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public float customerMoveSpeed = 5f;

    public static Customer instance;
    public static Customer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Customer>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(Customer).Name;
                    instance = obj.AddComponent<Customer>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public GameObject[] pathPoint;
    public GameObject[] chairPoint;
}
