using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using TMPro;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] PlayerData playerDataEasy;
    [SerializeField] PlayerData playerDataHard;
    PlayerData playerData;
    [SerializeField]AudioClip playerHurtMusic;
    [SerializeField] CameraController camController;
    [SerializeField] public Transform CamFollowStart;
    [SerializeField] public Transform CamFollowEnd;

    [SerializeField] SpriteRenderer invincibalCircal;
    private float invinOriginAlpha;
    [SerializeField] SpriteRenderer CounterCircal;

    private Rigidbody2D rb;
    private BuffManager buffManager;

    float curFallingSpeed;
    float HorSpeedScalar = 1.0f;

    float inputHorizontal;
    float inputVertical;
    bool inputCounter;
    bool inputShoot;

    bool isCountering => CounterLastTimer > 0;
    bool isCounterCoolDown => CounterCDTimer <= 0;
    bool isInvincibal => invincibalTimer > 0;
    bool isFrozen => HorSpeedScalar == 0;

    float CounterCDTimer;
    float invincibalTimer;
    float CounterLastTimer;

    static private int hp ;
    public Action<int> HpChange;
    //-1:off 1:on
    private int cheatFlag=-1;

    static private int ballN=0;

    public GameObject myBallPre;

    public Transform SpawnedObjects;


    // public TextMeshProUGUI BallNumText;
    // public GameObject myballUIPre;

    public int Hp {
        get
        {
            return hp;
        }
        set
        {
            if (isInvincibal)
                return;

            if(cheatFlag>0) return;

            hp = value;
            if(hp == 0)
            {
                ballNum=0;
                GameoverMgr.Instance.Gameover();
            }
            HpChange?.Invoke(value);

        }

    }

    

    public int ballNum{
        get{
            // Debug.Log("Get_ballNum: "+ballN);
            return ballN;
        }
        set{
            // Debug.Log("Set_ballNum: "+ballN);
            ballN=(value>=0?value:0); 
            // UpdateBallNumUI(value);
            if(SetBallNumUI.Instance!=null)
                SetBallNumUI.Instance.UpdateUI(value);
        }
    }
    
    // private void UpdateBallNumUI(int value){
    //     Transform tempUI=GameObject.Find("MyBallUI").transform.Find("Text");
    //     TextMeshProUGUI BallNumText=tempUI.GetComponent<TextMeshProUGUI>();
    //     // if(BallNumText==null)   return;
    //     BallNumText.text=value.ToString();
    //     // Debug.Log("BallText:"+textCont.text);
    // }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        buffManager = GetComponent<BuffManager>();
        //Reset Timers
        CounterCDTimer = 0;
        CounterLastTimer = 0;
        invincibalTimer = 0;
        
        //getSO
        playerData = (playerDataHard != null && DifficultySetting.HardMode) ? playerDataHard:playerDataEasy;
        //read invincibal config
        invinOriginAlpha = invincibalCircal.color.a;
        //Reset Falling Speed
        curFallingSpeed = playerData.FallingSpeedInit;

        if (Instance!=null)
        {
            Destroy(Instance);
        }
        else
        {

            hp = playerData.hpInit;
            // BallNumText=null;
            ballNum=0;

            CameraController cmcc=Camera.main.GetComponent<CameraController>();
            cmcc.score=0f;
            
        }
        Instance = this;
        //
        HpChange += (i) =>
        {
            if (BGMPlay.instance != null)
            {
                BGMPlay.instance.PlayMusic(playerHurtMusic);
            }
        };


    }
    private void Start()
    {
        // camController = Camera.main.GetComponent<CameraController>();

        // Transform tempUI=GameObject.Find("MyBallUI").transform.Find("Text");

        // BallNumText=tempUI.GetComponent<TextMeshProUGUI>();
        
        // UpdateBallNumUI(ballNum);
        
    }
    private void FixedUpdate()
    {
        HandleMovement();

        HandleAllTimerCD();

        
    }
    private void Update()
    {

        // UpdateBallNumUI(ballNum);
        
        //Reset Horizontal Speed
        HorSpeedScalar = 1f;
        
        HandleInput();

        

        HandleCameraFollowLogic();

        HandleCheatOpe();

        HandleStatusDisplay();

        buffManager.HandleBuffEffect();

        HandleCounter();

        if(inputShoot&&ballNum>0){
            HandleShoot();
            ballNum--;
        }
            

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
            targetColor.a = invinOriginAlpha;
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
    private void OnDrawGizmosSelected()
    {
        if (playerData != null)
        {
            Gizmos.DrawSphere(transform.position, playerData.CounterRadius);
        }
    }
    private void HandleCounter()
    {
        if (!isFrozen && inputCounter && isCounterCoolDown)
        {
            //设置显示counter圈大小
            CounterCircal.transform.localScale = new Vector3(playerData.CounterRadius, playerData.CounterRadius, playerData.CounterRadius) * 0.26f;
            
            CounterLastTimer = playerData.CounterLastTime;
            CounterCDTimer = playerData.CounterCD;
        }
        else if (inputCounter)
        {
            Debug.Log("CDing");
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
                        Destroy(collider.gameObject);
                        // b.GetCountered(transform);
                        ballNum++;
                        //开启无敌时间 begin invincibal
                        SetInvincibal(playerData.InvincibalTime);
                    }
                }
            }
        }
    }
    public void SetInvincibal(float LastTime)
    {
        invincibalTimer = LastTime;
    }

    private void HandleCheatOpe()
    {
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            // Hp++;
            cheatFlag*=-1;
        }
    }

    private void HandleCameraFollowLogic()
    {
        bool targetIsCamFollow = CamFollowStart.position.y > transform.position.y && CamFollowEnd.position.y < transform.position.y;
        camController?.FollowPlayer(targetIsCamFollow);
    }

    

    private void HandleMovement()
    {
        //int airForce;
        //if(curFallingSpeed > playerData.FallingSpeedInit)

        curFallingSpeed -= inputVertical * playerData.VerticalSpeed * Time.deltaTime;
        curFallingSpeed = Math.Clamp(curFallingSpeed, playerData.MinFallSpeed, playerData.MaxFallSpeed);

        float curHorSpeed = inputHorizontal * playerData.HorizontalSpeed * HorSpeedScalar;

        rb.velocity = new Vector2( curHorSpeed, -curFallingSpeed);
    } 

    private void HandleShoot(){

        GameObject myball = Instantiate(myBallPre, SpawnedObjects) as GameObject;
        myball.transform.position=transform.position;

        Ball go=myball.GetComponent<Ball>();
        go.Friendly=true;

        go.transform.position = transform.position;

        ArrowAim arrow=transform.Find("Arrow").GetComponent<ArrowAim>();
        Vector3 dir=arrow.dir;

        go.SetGravity(0f);

        // go.SetVelocity(new Vector2(vxOffset, vyOffset));
        Vector2 tempV=new Vector2(dir.x, dir.y);

        go.SetVelocity(tempV*20);

        // rb.velocity -= tempV*100;
    }
    
    private void HandleInput()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputCounter = Input.GetKeyDown(KeyCode.Space);
        inputShoot = Input.GetMouseButtonUp(1);
    }

}

