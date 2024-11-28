using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    float inputHorizontal;
    bool inputCounter;
    [Tooltip("弹反半径")]
    [SerializeField] float CounterRadius = 5f;
    [Tooltip("弹反时长")]
    [SerializeField] float CounterLastTime = 0.5f;
    [Tooltip("无敌时长")]
    [SerializeField] float InvincibalTime = 3f;
    [Tooltip("弹反CD")]
    [SerializeField] float CounterCD = 1f;

    float CounterCDTimer;
    float invincibalTimer;
    float CounterLastTimer;
    bool isCountering => CounterLastTimer > 0;
    bool isCounterCoolDown => CounterCDTimer <= 0;
    bool isInvincibal => invincibalTimer > 0;
    

    Rigidbody2D rb;
    public float FallingSpeed = 8f;
    public float HorizontalSpeed = 2f;
    public Action<int> HpChange;
    
    public int Hp {
        get
        {
            return hp;
        }
        set
        {
            if (isInvincibal)
                return;

            hp = value;
            if(hp == 0)
            {
                GameoverMgr.Instance.Gameover();
            }
            HpChange?.Invoke(value);

        }

    }
    [SerializeField]private int hp;
    float HorSpeedScalar = 1.0f;

    private BuffManager buffManager;

    [SerializeField]CameraController camController;
    public Transform CamFollowStart;
    public Transform CamFollowEnd;

    [SerializeField] SpriteRenderer invincibalCircal;
    [SerializeField] SpriteRenderer CounterCircal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        buffManager = GetComponent<BuffManager>();
        //Reset Timers
        CounterCDTimer = 0;
        CounterLastTimer = 0;
        invincibalTimer = 0;

        

        if (Instance!=null)
            Destroy(Instance);
        Instance = this;
    }
    private void Start()
    {
        // camController = Camera.main.GetComponent<CameraController>();
        
    }
    private void FixedUpdate()
    {
        HandleMovement();

        HandleAllTimerCD();

        
    }
    private void Update()
    {
        //Reset Speed
        HorSpeedScalar = 1f;
        
        HandleInput();

        HandleCounter();

        HandleCameraFollowLogic();

        HandleCheatOpe();

        HandleStatusDisplay();

        buffManager.HandleBuffEffect();
    }
    public void ChangeSpeedHor(float scalar)
    {
        HorSpeedScalar = scalar;
    }
    private void HandleStatusDisplay()
    {
        Color targetColor;
        //invincibal
        if (isInvincibal)
        {
            targetColor = invincibalCircal.color;
            targetColor.a = 1f;
            invincibalCircal.color = targetColor;
        }
        else
        {
            targetColor = invincibalCircal.color;
            targetColor.a = 0;
            invincibalCircal.color = targetColor;
        }
        //Counter
        targetColor = CounterCircal.color;
        targetColor.a = CounterLastTimer / CounterLastTime;
        CounterCircal.color = targetColor;

    }

    private void HandleAllTimerCD()
    {
        if (!isCounterCoolDown)
        { 
            CounterCDTimer -= Time.deltaTime; 
        }
        
        if(invincibalTimer > 0)
        {
            invincibalTimer -= Time.deltaTime;
        }

        if(isCountering)
        { 
            CounterLastTimer -=Time.deltaTime;
        }
    }

    private void HandleCounter()
    {
        if (inputCounter && isCounterCoolDown)
        {
            //设置显示counter圈大小
            CounterCircal.transform.localScale = new Vector3(CounterRadius, CounterRadius, CounterRadius);

            CounterLastTimer = CounterLastTime;
            CounterCDTimer = CounterCD;
        }
        else if (inputCounter)
        {
            Debug.Log("CD中");
        }


        if (isCountering)
        {
            Vector2 Pos = new Vector2(transform.position.x, transform.position.y);
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(Pos, CounterRadius);
            foreach (Collider2D collider in collider2D)
            {
                if (collider.CompareTag("Ball"))
                {
                    Ball fb = collider.GetComponent<Ball>();
                    if (!fb.Friendly)
                    {
                        fb.Friendly = true;
                        fb.SetVelocity(-fb.GetVelocity());
                        //开启无敌时间 begin invincibal
                        invincibalTimer = InvincibalTime;
                    }
                }
            }
        }
    }

    private void HandleCheatOpe()
    {
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            Hp++;
        }
    }

    private void HandleCameraFollowLogic()
    {
        bool targetIsCamFollow = CamFollowStart.position.y > transform.position.y && CamFollowEnd.position.y < transform.position.y;
        camController?.FollowPlayer(targetIsCamFollow);
    }

    

    private void HandleMovement()
    {
        rb.velocity = new Vector2(inputHorizontal * HorizontalSpeed * HorSpeedScalar, -FallingSpeed);
    } 
    
    private void HandleInput()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputCounter = Input.GetKeyDown(KeyCode.Space);
    }

}
