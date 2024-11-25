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
    [SerializeField] float HorSpeedScalar = 1.0f;

    private BuffManager buffManager;

    CameraController camController;
    public Transform CamFollowStart;
    public Transform CamFollowEnd;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        buffManager = GetComponent<BuffManager>();


        if (Instance!=null)
            Destroy(Instance);
        Instance = this;
    }
    private void Start()
    {
        camController = Camera.main.GetComponent<CameraController>();
        
    }
    private void Update()
    {
        //Reset Speed
        HorSpeedScalar = 1f;
        
        HandleInput();
        HandleCameraFollowLogic();
        buffManager.HandleBuffEffect();
    }

    private void HandleCameraFollowLogic()
    {
        bool targetIsCamFollow = CamFollowStart.position.y > transform.position.y && CamFollowEnd.position.y < transform.position.y;
        camController.FollowPlayer(targetIsCamFollow);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        rb.velocity = new Vector2(inputHorizontal * HorizontalSpeed * HorSpeedScalar, -FallingSpeed);
    } 
    public void ChangeSpeedHor(float scalar)
    {
        HorSpeedScalar = scalar;
    }
    private void HandleInput()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
    }
}
