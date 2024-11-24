using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    float inputHorizontal;
    Rigidbody2D rb;
    public float FallingSpeed = 8f;
    public float HorizontalSpeed = 2f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(Instance!=null)
            Destroy(Instance);
        Instance = this;
    }
    private void Update()
    {
        HandleInput();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {

        rb.velocity = new Vector2(inputHorizontal * HorizontalSpeed, -FallingSpeed);
    }

    private void HandleInput()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
    }
}
