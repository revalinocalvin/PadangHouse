using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPathing2 : MonoBehaviour
{
    /*Missing
    Revert null on chairPoint[i] when finish eating
    */
    //private bool enabled = false;
    private bool onTable = false;
    private int pathCounter = 0;
    
    Vector3 direction;

    void Start()
    {
        // Disable script at start
        this.enabled = false;
        // Start coroutine to enable script after 5 seconds
        StartCoroutine(EnableScriptAfterDelay(5));
    }

    IEnumerator EnableScriptAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.enabled = true;
    }

    void Update()
    {
        if (!onTable)
        {
            MoveToChair();
        }
    }

    void MoveToChair()
    {
        if (Customer.instance != null)
        {
            if (Customer.instance.chairPoint[0] != null)
            {
                MoveToChair1();
            }
            else if (Customer.instance.chairPoint[1] != null)
            {
                MoveToChair2();
            }
            else if (Customer.instance.chairPoint[2] != null)
            {
                MoveToChair3();
            }
            else if (Customer.instance.chairPoint[3] != null)
            {
                MoveToChair4();
            }
        }
    }

    void MoveToChair1()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.instance.pathPoint[0].transform.position - transform.position).normalized;
        }
        else if (pathCounter == 1)
        {
            direction = (Customer.instance.pathPoint[1].transform.position - transform.position).normalized;
        }
        else
        {
            direction = (Customer.instance.chairPoint[0].transform.position - transform.position).normalized;
        }

        if (Vector2.Distance(Customer.instance.pathPoint[0].transform.position, transform.position) <= 0.1f && pathCounter == 0)
        {
            pathCounter++;
        }
        else if (Vector2.Distance(Customer.instance.pathPoint[1].transform.position, transform.position) <= 0.1f && pathCounter == 1)
        {
            pathCounter++;
        }
        else if (Vector2.Distance(Customer.instance.chairPoint[0].transform.position, transform.position) <= 0.1f)
        {
            Customer.instance.chairPoint[0] = null;
            onTable = true;
            this.enabled = false;
        }

        transform.position += direction * Customer.instance.customerMoveSpeed * Time.deltaTime;
    }

    void MoveToChair2()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.instance.chairPoint[1].transform.position - transform.position).normalized;
        }
        if (Vector2.Distance(Customer.instance.chairPoint[1].transform.position, transform.position) <= 0.1f)
        {
            Customer.instance.chairPoint[1] = null;
            onTable = true;
            this.enabled = false;
        }

        transform.position += direction * Customer.instance.customerMoveSpeed * Time.deltaTime;
    }

    void MoveToChair3()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.instance.chairPoint[2].transform.position - transform.position).normalized;
        }
        if (Vector2.Distance(Customer.instance.chairPoint[2].transform.position, transform.position) <= 0.1f)
        {
            Customer.instance.chairPoint[2] = null;
            onTable = true;
            this.enabled = false;
        }

        transform.position += direction * Customer.instance.customerMoveSpeed * Time.deltaTime;
    }

    void MoveToChair4()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.instance.pathPoint[0].transform.position - transform.position).normalized;
        }
        else if (pathCounter == 1)
        {
            direction = (Customer.instance.pathPoint[2].transform.position - transform.position).normalized;
        }
        else
        {
            direction = (Customer.instance.chairPoint[3].transform.position - transform.position).normalized;
        }

        if (Vector2.Distance(Customer.instance.pathPoint[0].transform.position, transform.position) <= 0.1f && pathCounter == 0)
        {
            pathCounter++;
        }
        else if (Vector2.Distance(Customer.instance.pathPoint[2].transform.position, transform.position) <= 0.1f && pathCounter == 1)
        {
            pathCounter++;
        }
        else if (Vector2.Distance(Customer.instance.chairPoint[3].transform.position, transform.position) <= 0.1f)
        {
            Customer.instance.chairPoint[3] = null;
            onTable = true;
            this.enabled = false;
        }

        transform.position += direction * Customer.instance.customerMoveSpeed * Time.deltaTime;
    }
}
