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
        else if (chairNumber == 3)
        {
            MoveToChair3();
        }
        else if (chairNumber == 4)
        {
            MoveToChair4();
        }

        transform.position += direction * Customer.Instance.customerMoveSpeed * Time.deltaTime;
    }

    private Vector3 DirectionToChair(int index, bool checkDistance)
    {
        if (!checkDistance)
        {
            return Customer.Instance.chairPoint[index].transform.position;
        }
        else
        {
            return direction = (Customer.Instance.chairPoint[index].transform.position - transform.position).normalized;
        }
    }

    void MoveToChair1()
    {
        if (pathCounter == 0)
        {
            DirectionToChair(0, true);
        }
        if (Vector2.Distance(DirectionToChair(0, false), transform.position) <= 0.1f)
        {
            onChair = true;
        }
    }

    void MoveToChair2()
    {
        if (pathCounter == 0)
        {
            DirectionToChair(1, true);
        }
        if (Vector2.Distance(DirectionToChair(1, false), transform.position) <= 0.1f)
        {
            onChair = true;
        }
    }

    void MoveToChair3()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.Instance.pathPoint[0].transform.position - transform.position).normalized;
        }
        else if (pathCounter == 1)
        {
            DirectionToChair(2, true);
        }

        if (Vector2.Distance(Customer.Instance.pathPoint[0].transform.position, transform.position) <= 0.1f && pathCounter == 0)
        {
            pathCounter++;
        }
        else if (Vector2.Distance(DirectionToChair(2, false), transform.position) <= 0.1f && pathCounter == 1)
        {
            onChair = true;
        }
    }

    void MoveToChair4()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.Instance.pathPoint[0].transform.position - transform.position).normalized;
        }
        else if (pathCounter == 1)
        {
            DirectionToChair(3, true);
        }

        if (Vector2.Distance(Customer.Instance.pathPoint[0].transform.position, transform.position) <= 0.1f && pathCounter == 0)
        {
            pathCounter++;
        }
        else if (Vector2.Distance(DirectionToChair(3, false), transform.position) <= 0.1f && pathCounter == 1)
        {
            onChair = true;
        }
    }
}
