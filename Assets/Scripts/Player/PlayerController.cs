using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] PlayerData playerData;
    
    [SerializeField] CameraController camController;
    [SerializeField] public Transform CamFollowStart;
    [SerializeField] public Transform CamFollowEnd;

    [SerializeField] SpriteRenderer invincibalCircal;
    [SerializeField] SpriteRenderer CounterCircal;

    Rigidbody2D rb;
    private BuffManager buffManager;

    float curFallingSpeed;
    float HorSpeedScalar = 1.0f;

    float inputHorizontal;
    float inputVertical;
    bool inputCounter;

    bool isCountering => CounterLastTimer > 0;
    bool isCounterCoolDown => CounterCDTimer <= 0;
    bool isInvincibal => invincibalTimer > 0;
    bool isFrozen => HorSpeedScalar == 0;

    float CounterCDTimer;
    float invincibalTimer;
    float CounterLastTimer;

    static private int hp ;
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
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        buffManager = GetComponent<BuffManager>();
        //Reset Timers
        CounterCDTimer = 0;
        CounterLastTimer = 0;
        invincibalTimer = 0;
        //Reset Falling Speed
        curFallingSpeed = playerData.FallingSpeedInit;

        if (Instance!=null)
        {
            Destroy(Instance);
        }
        else
        {
            hp = playerData.hpInit;
        }
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
        //Reset Horizontal Speed
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
        targetColor.a = CounterLastTimer / playerData.CounterLastTime;
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
        if (!isFrozen && inputCounter && isCounterCoolDown)
        {
            //设置显示counter圈大小
            CounterCircal.transform.localScale = new Vector3(playerData.CounterRadius, playerData.CounterRadius, playerData.CounterRadius);

            CounterLastTimer = playerData.CounterLastTime;
            CounterCDTimer = playerData.CounterCD;
        }
        else if (inputCounter)
        {
            Debug.Log("CD中");
        }


        if (isCountering)
        {
            Vector2 Pos = new Vector2(transform.position.x, transform.position.y);
            Collider2D[] collider2D = Physics2D.OverlapCircleAll(Pos, playerData.CounterRadius);
            foreach (Collider2D collider in collider2D)
            {
                if (collider.CompareTag("Ball"))
                {
                    Ball b = collider.GetComponent<Ball>();
                    if (!b.Friendly)
                    {
                        
                        b.GetCountered(transform);
                        //开启无敌时间 begin invincibal
                        invincibalTimer = playerData.InvincibalTime;
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
        curFallingSpeed -= inputVertical * playerData.VerticalSpeed * Time.deltaTime;
        curFallingSpeed = Math.Clamp(curFallingSpeed, playerData.MinFallSpeed, playerData.MaxFallSpeed);

        float curHorSpeed = inputHorizontal * playerData.HorizontalSpeed * HorSpeedScalar;

        rb.velocity = new Vector2( curHorSpeed, -curFallingSpeed);
    } 
    
    private void HandleInput()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputCounter = Input.GetKeyDown(KeyCode.Space);
    }

}
