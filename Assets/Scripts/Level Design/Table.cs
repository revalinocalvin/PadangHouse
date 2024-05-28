using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    
    public HashSet<GameObject> customers = new HashSet<GameObject>();
    public float radius = 3f;

    public void FindCustomer()
    {                
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        
        foreach (Collider2D collider in colliders)
        {            
            GameObject obj = collider.gameObject;
            
            if (obj.CompareTag("CustomerGroup"))
            {                                
                customers.Add(obj); 
            }
        }

        foreach (GameObject customer in customers)
        {
            CustomerFoodMerged customerOrder = customer.GetComponent<CustomerFoodMerged>();
            customerOrder.TakeOrder();
        }
        
    }

}
