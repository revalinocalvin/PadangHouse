using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPathing : MonoBehaviour
{
    private bool onChair = false;
    private bool chairDecided = false;
    private int chairNumber;
    private int pathCounter = 0;
    
    Vector3 direction;

    void Update()
    {
        if (!chairDecided)
        {
            DecideChair();
        }

        if (chairDecided && !onChair)
        {
            MoveToChair();
        }
    }

    void DecideChair()
    {
        for (int i = 0; i < Customer.Instance.chairAvailable.Length; i++)
        {
            if (Customer.Instance.chairAvailable[i])
            {
                chairDecided = true;
                Customer.Instance.chairAvailable[i] = false;
                chairNumber = i + 1;
                break;
            }
        }
    }

    void MoveToChair()
    {
        if (chairNumber == 1)
        {
            MoveToChair1();
        }
        else if (chairNumber == 2)
        {
            MoveToChair2();
        }
        /*else if (chairNumber == 3)
        {
            MoveToChair3();
        }
        else if (chairNumber == 4)
        {
            MoveToChair4();
        }*/
    }

    void MoveToChair1()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.Instance.chairPoint[0].transform.position - transform.position).normalized;
        }
        if (Vector2.Distance(Customer.Instance.chairPoint[0].transform.position, transform.position) <= 0.1f)
        {
            onChair = true;
        }

        transform.position += direction * Customer.Instance.customerMoveSpeed * Time.deltaTime;
    }

    void MoveToChair2()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.Instance.pathPoint[0].transform.position - transform.position).normalized;
        }
        else if (pathCounter == 1)
        {
            direction = (Customer.Instance.chairPoint[1].transform.position - transform.position).normalized;
        }

        if (Vector2.Distance(Customer.Instance.pathPoint[0].transform.position, transform.position) <= 0.1f && pathCounter == 0)
        {
            pathCounter++;
        }
        else if (Vector2.Distance(Customer.Instance.chairPoint[1].transform.position, transform.position) <= 0.1f && pathCounter == 1)
        {
            onChair = true;
        }

        transform.position += direction * Customer.Instance.customerMoveSpeed * Time.deltaTime;
    }

    /*void MoveToChair3()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.Instance.chairPoint[2].transform.position - transform.position).normalized;
        }
        if (Vector2.Distance(Customer.Instance.chairPoint[2].transform.position, transform.position) <= 0.1f)
        {
            onChair = true;
        }

        transform.position += direction * Customer.Instance.customerMoveSpeed * Time.deltaTime;
    }

    void MoveToChair4()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.Instance.pathPoint[0].transform.position - transform.position).normalized;
        }
        else if (pathCounter == 1)
        {
            direction = (Customer.Instance.pathPoint[2].transform.position - transform.position).normalized;
        }
        else
        {
            direction = (Customer.Instance.chairPoint[3].transform.position - transform.position).normalized;
        }

        if (Vector2.Distance(Customer.Instance.pathPoint[0].transform.position, transform.position) <= 0.1f && pathCounter == 0)
        {
            pathCounter++;
        }
        else if (Vector2.Distance(Customer.Instance.pathPoint[2].transform.position, transform.position) <= 0.1f && pathCounter == 1)
        {
            pathCounter++;
        }
        else if (Vector2.Distance(Customer.Instance.chairPoint[3].transform.position, transform.position) <= 0.1f)
        {
            onChair = true;
        }

        transform.position += direction * Customer.Instance.customerMoveSpeed * Time.deltaTime;
    }*/
}
