using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireballs : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D mCollider;
    PlayerController Player;
    BallSpawner ballSpawner;

    public float disappearDistance = 20f;
    
    private Vector2 Gravity;

    private bool isHostile;
    public bool Friendly
    {
        get
        {
            return !isHostile;
        }
        set
        {
            isHostile = !value;
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mCollider = GetComponent<CircleCollider2D>();
        isHostile = true;
    }
    private void Start()
    {
        Player = PlayerController.Instance;
    }
    private void Update()
    {
        if (Player == null)
        {
            return;
        }
        //Gravity
        rb.velocity += Gravity;

        HandleRotation();
        //Disappear if far away
        Vector3 Distance = Player.transform.position - transform.position;
        if (Distance.magnitude > disappearDistance)
        {
            Destroy(gameObject);
        }
    }
    
    protected virtual void HandleRotation()
    {
        Vector3 origin = new Vector3(0,-1,0);
        Vector3 v = rb.velocity;
        float angle = Vector3.Angle(origin, v);
        if(v.x < 0) 
            angle = -angle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetSpawner(BallSpawner bs)
    {
        ballSpawner = bs;
    }
    public void SetGravity(float gravity)
    {
        Gravity = new Vector2(0,-gravity);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.GetComponent<PlayerController>().Hp--;
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if(other.CompareTag("Ball"))
        {
            if (other.GetComponent<Fireballs>().Friendly != this.Friendly)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        ballSpawner.OnBallExit(this);
    }
}
