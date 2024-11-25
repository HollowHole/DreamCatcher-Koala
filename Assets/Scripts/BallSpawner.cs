
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Tooltip("生成对象")]
    public GameObject SpawningBalls;

    [SerializeField]List<Fireballs> ExistingBalls;
    public Transform SpawnedObjects;

    public int MaxBallCount = 5;

    public float SpawnPointOffsetH;

    public float minVyOffset;
    public float maxVyOffset;
    public float vxRange;

    private float PlayerFallingSpeed;

    public float SpawnCD = 2f;
    private float SpawnTimer = 0;
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
