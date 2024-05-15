using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerExit : MonoBehaviour
{
    Vector3 direction;
    public GameObject player; // Reference to the player GameObject
    private int pathCounter = 0; // Added this to avoid compile errors. Adjust as needed.
    private PlayerInteract playerInteract;

    // Start is called before the first frame update
    void Start()
    {
        playerInteract = player.GetComponent<PlayerInteract>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveToExit(GameObject customer)
    {
        StartCoroutine(DelayedMove());
    }

    private IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(5f);
        yield return Move();
    }

    private IEnumerator Move()
    {
        if (pathCounter == 0)
        {
            direction = (Customer.instance.exitPoint[0].transform.position - transform.position).normalized;
        }

        while (Vector2.Distance(Customer.instance.exitPoint[0].transform.position, transform.position) > 0.1f)
        {
            transform.position += direction * Customer.instance.customerMoveSpeed * Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Movement finished
        // onTable = false; // Uncomment if necessary
        this.enabled = false;
    }
}
