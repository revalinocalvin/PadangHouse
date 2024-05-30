using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInteract PI;

    [SerializeField] private float playerMoveSpeed;
    Rigidbody2D body;
    public float facing;
    public Animator animator;
    private Vector3 moveVector;

    void Start()
    {
        PI = GetComponent<PlayerInteract>();
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        WASDMovement();
        Animation();
    }

    void WASDMovement()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        moveVector = new Vector3(xInput, yInput, 0).normalized;

        FaceDirection(moveVector);

        body.MovePosition(new Vector2(transform.position.x + moveVector.x * playerMoveSpeed * Time.deltaTime, transform.position.y + moveVector.y * playerMoveSpeed * Time.deltaTime));
    }

    void Animation()
    {
        animator.SetFloat("Horizontal", moveVector.x);
        animator.SetFloat("Vertical", moveVector.y);
        animator.SetFloat("Speed", moveVector.sqrMagnitude);
        animator.SetFloat("Facing", facing);
    }

    void FaceDirection(Vector3 moveVector)
    {
        if (moveVector.x > 0)
        {
            PI.grabPoint.localPosition = new Vector3(1, 0, 0);
            facing = 3;
        }
        else if (moveVector.x < 0)
        {
            PI.grabPoint.localPosition = new Vector3(-1, 0, 0);
            facing = 2;
        }
        else if (moveVector.y < 0)
        {
            PI.grabPoint.localPosition = new Vector3(0, -1, 0);
            facing = 1;
        }
        else if (moveVector.y > 0)
        {
            PI.grabPoint.localPosition = new Vector3(0, 1, 0);
            facing = 4;
        }
    }
}
