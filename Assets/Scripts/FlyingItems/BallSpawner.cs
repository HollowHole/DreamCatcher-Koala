
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Tooltip("生成对象")]
    public GameObject SpawningBalls;

    [Tooltip("场景中存在的对象")]
    [SerializeField]List<Fireballs> ExistingBalls;

    [Tooltip("生成物体挂在该对象下")]
    public Transform SpawnedObjects;

    [Tooltip("场景中该对象最大存在数量")]
    public int MaxBallCount = 5;
    [Tooltip("生成位置的左右最大偏移量")]
    public float SpawnPointOffsetH;
    [Tooltip("生成时最小y轴速度")]
    public float minVyOffset;
    [Tooltip("生成时最大y轴速度")]
    public float maxVyOffset;
    [Tooltip("生成时最大x轴速度")]
    public float vxRange;

    private float PlayerFallingSpeed;
    [Tooltip("最小生成间隔")]
    public float SpawnCD = 2f;
    private float SpawnTimer = 0;
    [Tooltip("生成物体受到的最小重力")]
    public float minGravity;
    [Tooltip("生成物体受到的最大重力")]
    public float maxGravity;

    private void Awake()
    {
        
    }
    private void Start()
    {
        PlayerFallingSpeed = PlayerController.Instance.FallingSpeed;
    }
    private void Update()
    {
        if(SpawnTimer > 0)
        {
            SpawnTimer -= Time.deltaTime;
            return;
        }
        if (ExistingBalls.Count < MaxBallCount)
        {
            DoSpawn();
        }
    }

    private void DoSpawn()
    {
        Fireballs go = Instantiate(SpawningBalls, SpawnedObjects).GetComponent<Fireballs>();

        go.SetSpawner(this);
        //SpawnOffset
        float SpawnOffset = Random.Range(-SpawnPointOffsetH,SpawnPointOffsetH);
        go.transform.position = transform.position + new Vector3(SpawnOffset, 0, 0);
        //Set Velocity
        float vyOffset = Random.Range(minVyOffset, maxVyOffset);
        float vxOffset = Random.Range(-vxRange, vxRange);
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(vxOffset, vyOffset);
        //Set Gravity
        go.SetGravity(Random.Range(minGravity, maxGravity));
        //Add to list
        ExistingBalls.Add(go);
        //reset CD
        SpawnTimer = SpawnCD;
    }

    public void OnBallExit(Fireballs balls)
    {
        ExistingBalls.Remove(balls);
    }
}
