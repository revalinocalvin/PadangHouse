using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPathing : MonoBehaviour
{
    public bool onChair = false;
    private bool chairDecided = false;
    public int chairNumber;

    private int pathCounter = 0;

    public bool eatingFinished = false;
    
    private Vector3 direction;

    void Update()
    {
        if (!chairDecided && !eatingFinished)
        {
            DecideChair();
        }

        if (chairDecided && !onChair)
        {
            MoveToChair();
        }

        if (eatingFinished)
        {
            onChair = false;
            chairDecided = false;
            MoveToExit();
        }
    }

    void DecideChair()
    {
        for (int i = 0; i < CustomerManager.Instance.chairAvailable.Length; i++)
        {
            if (CustomerManager.Instance.chairAvailable[i])
            {
                chairDecided = true;
                CustomerManager.Instance.chairAvailable[i] = false;
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

        transform.position += direction * CustomerManager.Instance.customerMoveSpeed * Time.deltaTime;
    }

    private Vector3 DirectionToChair(int index)
    {
        return direction = (CustomerManager.Instance.chairPoint[index].transform.position - transform.position).normalized;
    }

    private bool ArrivedOnChairPoint(int index)
    {
        return Vector2.Distance(CustomerManager.Instance.chairPoint[index].transform.position, transform.position) <= 0.1f;
    }

    private Vector3 DirectionToPath(int index)
    {
        return direction = (CustomerManager.Instance.pathPoint[index].transform.position - transform.position).normalized;
    }

    private bool ArrivedOnPathPoint(int index)
    {
        return Vector2.Distance(CustomerManager.Instance.pathPoint[index].transform.position, transform.position) <= 0.1f;
    }

    private Vector3 DirectionToExitPoint()
    {
        return direction = (CustomerManager.Instance.exitPoint.transform.position - transform.position).normalized;
    }

    private bool ArrivedOnExitPoint()
    {
        return Vector2.Distance(CustomerManager.Instance.exitPoint.transform.position, transform.position) <= 0.1f;
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

    void MoveToExit()
    {
        if (chairNumber == 1 || chairNumber == 2 || chairNumber == 5 || chairNumber == 6)
        {
            MoveToExit1();
        }
        else if (chairNumber == 3 || chairNumber == 4 || chairNumber == 7 || chairNumber == 8)
        {
            MoveToExit2();
        }

        transform.position += direction * CustomerManager.Instance.customerMoveSpeed * Time.deltaTime;
    }

    void MoveToExit1()
    {
        DirectionToExitPoint();
    }

    void MoveToExit2()
    {
        if (pathCounter == 1)
        {
            DirectionToPath(0);
        }
        else if (pathCounter == 0)
        {
            DirectionToExitPoint();
        }

        if (ArrivedOnPathPoint(0) && pathCounter == 1)
        {
            pathCounter--;
        }
        else if (ArrivedOnExitPoint())
        {
            Destroy(this.gameObject);
        }
    }
}
