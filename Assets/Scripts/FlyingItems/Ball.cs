using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    protected Rigidbody2D rb;
    CircleCollider2D mCollider;
    PlayerController Player;
    BallSpawner ballSpawner;
    

    public float disappearDistance = 20f;

    protected Vector2 Gravity;
    protected SpriteRenderer mImg;
    protected Animator animator;

    protected bool isHostile;
    public bool Friendly
    {
        get
        {
            return !isHostile;
        }
        set
        {
            isHostile = !value;
            UpdateView();
        }
    }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mCollider = GetComponent<CircleCollider2D>();
        mImg = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
    public abstract void UpdateView();

    protected virtual void HandleRotation()
    {
        Vector3 origin = new Vector3(0, -1, 0);
        Vector3 v = rb.velocity;
        float angle = Vector3.Angle(origin, v);
        if (v.x < 0)
            angle = -angle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetSpawner(BallSpawner bs)
    {
        ballSpawner = bs;
    }
    public void SetGravity(float gravity)
    {
        Gravity = new Vector2(0, -gravity);
    }
    public void SetGravity(Vector2 gravity)
    {
        Gravity = gravity;
    }
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }
    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (isHostile && other.CompareTag("Player"))
        {
            Destroy(gameObject);
            HitPlayer(other.GetComponent<PlayerController>());
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ball"))
        {
            if (other.GetComponent<Ball>().Friendly != this.Friendly)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
    public virtual void GetCountered(Transform playerTrans )
    {
        Friendly = true;
        rb.velocity = -rb.velocity;
    }
    protected virtual void HitPlayer(PlayerController player)
    {
        player.Hp--;
    }
    protected void OnDestroy()
    {
        ballSpawner?.OnBallExit(this);
    }
}
