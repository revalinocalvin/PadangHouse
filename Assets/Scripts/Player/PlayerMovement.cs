using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInteraction PI;

    [SerializeField] private float playerMoveSpeed;
    Rigidbody2D body;

    void Start()
    {
        PI = GetComponent<PlayerInteraction>();
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

        Vector3 moveVector = new Vector3(xInput, yInput, 0);

        FaceDirection(moveVector);

        body.MovePosition(new Vector2(transform.position.x + moveVector.x * playerMoveSpeed * Time.deltaTime, transform.position.y + moveVector.y * playerMoveSpeed * Time.deltaTime));
    }

    void FaceDirection(Vector3 moveVector)
    {
        if (moveVector.x > 0)
        {
            PI.holdPoint.localPosition = new Vector3(1, 0, 0);
            PI.rayDirection = Vector2.right;
        }
        else if (moveVector.x < 0)
        {
            PI.holdPoint.localPosition = new Vector3(-1, 0, 0);
            PI.rayDirection = Vector2.left;
        }
        else if (moveVector.y < 0)
        {
            PI.holdPoint.localPosition = new Vector3(0, -1, 0);
            PI.rayDirection = Vector2.down;
        }
        else if (moveVector.y > 0)
        {
            PI.holdPoint.localPosition = new Vector3(0, 1, 0);
            PI.rayDirection = Vector2.up;
        }
    }
}
