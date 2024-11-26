using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fireballs : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D mCollider;
    PlayerController Player;
    public float disappearDistance = 20f;
    BallSpawner ballSpawner;
    private Vector2 Gravity;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mCollider = GetComponent<CircleCollider2D>(); 
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
        rb.velocity += Gravity;
        Vector3 Distance = Player.transform.position - transform.position;
        if (Distance.magnitude > disappearDistance)
        {
            Destroy(gameObject);
        }
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
        //else if (other.CompareTag("Wall"))  //TODO
    }
    private void OnDestroy()
    {
        ballSpawner.OnBallExit(this);
    }
}
