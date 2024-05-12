using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed;
    Rigidbody2D body;

    void Start()
    {
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

        body.MovePosition(new Vector2(transform.position.x + moveVector.x * playerMoveSpeed * Time.deltaTime, transform.position.y + moveVector.y * playerMoveSpeed * Time.deltaTime));
    }
}
