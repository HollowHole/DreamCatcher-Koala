
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Tooltip("生成对象")]
    public GameObject SpawningBalls;

    [Tooltip("场景中存在的对象")]
    [SerializeField]protected List<Ball> ExistingBalls;

    [Tooltip("生成物体挂在该对象下")]
    public Transform SpawnedObjects;

    [SerializeField]protected SpawnerData spawnerDataEasy;
    [SerializeField] protected SpawnerData spawnerDataHard;
    protected SpawnerData spawnerData;
    protected float SpawnTimer = 0;
    

    protected void Awake()
    {
        //getSO
        spawnerData = (spawnerDataHard != null && DifficultySetting.HardMode) ? spawnerDataHard : spawnerDataEasy;
    }
    protected void Start()
    {
       
    }
    protected void Update()
    {
        if(SpawnTimer > 0)
        {
            SpawnTimer -= Time.deltaTime;
            return;
        }
        if (ExistingBalls.Count < spawnerData.MaxBallCount)
        {
            DoSpawn();
        }
    }

    protected virtual void DoSpawn()
    {
        Ball go = Instantiate(SpawningBalls, SpawnedObjects).GetComponent<Ball>();

        go.SetSpawner(this);
        //SpawnOffset
        float SpawnOffset = Random.Range(-spawnerData.SpawnPointOffsetH, spawnerData.SpawnPointOffsetH);
        go.transform.position = transform.position + new Vector3(SpawnOffset, 0, 0);
        //Set Velocity
        float vyOffset = Random.Range(spawnerData.minVyOffset, spawnerData.maxVyOffset);
        float vxOffset = Random.Range(-spawnerData.vxRange, spawnerData.vxRange);
        go.SetVelocity(new Vector2(vxOffset, vyOffset));
        //Set Gravity
        go.SetGravity(Random.Range(spawnerData.minGravity, spawnerData.maxGravity));
        //Add to list
        ExistingBalls.Add(go);
        //reset CD
        SpawnTimer = spawnerData.SpawnCD;
    }

    public void OnBallExit(Ball balls)
    {
        ExistingBalls.Remove(balls);
    }
}
