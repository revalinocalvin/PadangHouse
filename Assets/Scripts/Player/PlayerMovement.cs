using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInteract PI;
    GameObject rp;

    [SerializeField] private float playerMoveSpeed;
    Rigidbody2D body;

    void Start()
    {
        PI = GetComponent<PlayerInteract>();
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        WASDMovement();
    }

    void WASDMovement()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector3 moveVector = new Vector3(xInput, yInput, 0).normalized;

        FaceDirection(moveVector);

        body.MovePosition(new Vector2(transform.position.x + moveVector.x * playerMoveSpeed * Time.deltaTime, transform.position.y + moveVector.y * playerMoveSpeed * Time.deltaTime));
    }

    void FaceDirection(Vector3 moveVector)
    {
        if (moveVector.x > 0)
        {
            PI.grabPoint.localPosition = new Vector3(1, 0, 0);
        }
        else if (moveVector.x < 0)
        {
            PI.grabPoint.localPosition = new Vector3(-1, 0, 0);
        }
        else if (moveVector.y < 0)
        {
            PI.grabPoint.localPosition = new Vector3(0, -1, 0);
        }
        else if (moveVector.y > 0)
        {
            PI.grabPoint.localPosition = new Vector3(0, 1, 0);
        }
    }
}
