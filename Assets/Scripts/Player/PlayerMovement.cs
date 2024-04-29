using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        WASDMovement();
    }

    void WASDMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * playerMoveSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.right * -playerMoveSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * playerMoveSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.up * -playerMoveSpeed * Time.deltaTime;

        }
    }
}
