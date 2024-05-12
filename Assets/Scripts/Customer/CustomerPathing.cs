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

    private Vector3 DirectionToChair(int index)
    {
        return direction = (Customer.Instance.chairPoint[index].transform.position - transform.position).normalized;
    }

    private bool ArrivedOnChairPoint(int index)
    {
        return Vector2.Distance(Customer.Instance.chairPoint[index].transform.position, transform.position) <= 0.1f;
    }

    private Vector3 DirectionToPath(int index)
    {
        return direction = (Customer.Instance.pathPoint[index].transform.position - transform.position).normalized;
    }

    private bool ArrivedOnPathPoint(int index)
    {
        return Vector2.Distance(Customer.Instance.pathPoint[index].transform.position, transform.position) <= 0.1f;
    }

    void MoveToChair1()
    {
        if (pathCounter == 0)
        {
            DirectionToChair(0);
        }
        if (ArrivedOnChairPoint(0))
        {
            onChair = true;
        }
    }

    void MoveToChair2()
    {
        if (pathCounter == 0)
        {
            DirectionToChair(1);
        }
        if (ArrivedOnChairPoint(1))
        {
            onChair = true;
        }
    }

    void MoveToChair3()
    {
        if (pathCounter == 0)
        {
            DirectionToPath(0);
        }
        else if (pathCounter == 1)
        {
            DirectionToChair(2);
        }

        if (ArrivedOnPathPoint(0) && pathCounter == 0)
        {
            pathCounter++;
        }
        else if (ArrivedOnChairPoint(2) && pathCounter == 1)
        {
            onChair = true;
        }
    }

    void MoveToChair4()
    {
        if (pathCounter == 0)
        {
            DirectionToPath(0);
        }
        else if (pathCounter == 1)
        {
            DirectionToChair(3);
        }

        if (ArrivedOnPathPoint(0) && pathCounter == 0)
        {
            pathCounter++;
        }
        else if (ArrivedOnChairPoint(3) && pathCounter == 1)
        {
            onChair = true;
        }
    }
}
