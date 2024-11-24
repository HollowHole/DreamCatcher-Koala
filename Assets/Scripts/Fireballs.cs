using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireballs : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D mCollider;
    PlayerController Player;
    public float disappearDistance = 20f;
    BallSpawner ballSpawner;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        ballSpawner.OnBallExit(this);
    }
}
