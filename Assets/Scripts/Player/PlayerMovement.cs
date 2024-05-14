using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    public GameObject gp;
    public GameObject rp;
    public GameObject eye;
    private Vector2 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gp = GameObject.Find("Player/GrabPosition");
        rp = GameObject.Find("Player/RayPosition");
        eye = GameObject.Find("Player/Eye");
    }
    
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
        Direction();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);   
    }

    void Direction()
    {
        if (moveDirection.x > 0)
        {
            Debug.Log("Right");
            rp.transform.localPosition = new Vector2(1.0f, 0.0f);
            gp.transform.localPosition = new Vector2(0.7f, 0.0f);
            eye.transform.localPosition = new Vector2(0.216f, 0.585f);
        }

        else if (moveDirection.x < 0)
        {
            Debug.Log("Left");
            rp.transform.localPosition = new Vector2(-1.0f, 0.0f);
            gp.transform.localPosition = new Vector2(-0.7f, 0.0f);
            eye.transform.localPosition = new Vector2(-0.216f, 0.585f);
        }

        else if (moveDirection.y > 0)
        {
            Debug.Log("Up");
            rp.transform.localPosition = new Vector2(0.0f, 1.3f);
            gp.transform.localPosition = new Vector2(0.0f, 1.2f);
        }

        else if (moveDirection.y < 0)
        {
            Debug.Log("Down");
            rp.transform.localPosition = new Vector2(0.0f, -1.3f);
            gp.transform.localPosition = new Vector2(0.0f, -1.2f);
        }
    }
}

