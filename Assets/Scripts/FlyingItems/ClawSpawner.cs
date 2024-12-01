using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawSpawner : BallSpawner
{
    [SerializeField] float CameraWidth;
    ClawSpawnerData clawData;
    new void Awake()
    {
        base.Awake();
        clawData = spawnerData as ClawSpawnerData;
    }
    // Update is called once per frame
    new void Update()
    {
        base.Update();
        
    }
    protected override void DoSpawn()
    {
        Ball go = Instantiate(SpawningBalls, SpawnedObjects).GetComponent<Ball>();

        go.SetSpawner(this);
        //SpawnOffset
        float SpawnOffsetV = Random.Range(clawData.SpawnOffsetVerticalMin, clawData.SpawnOffsetVerticalMax);
        int direction = Random.value > 0.5 ? 1 : -1;
        go.transform.position = transform.position + new Vector3(direction*CameraWidth/2, -SpawnOffsetV, -transform.position.z);
        //
        Claw claw = go.GetComponent<Claw>();
        claw.SetDirection(direction ==1 ? Claw.Direction.Right : Claw.Direction.Left);
        if (direction == -1)
            claw.transform.localScale = new Vector3(-1, 1, 1);
        claw.Length = Random.Range(clawData.minLength, clawData.maxLength);
        claw.strechOutTime = clawData.ClawStrechOutTime;
        claw.strechBackTime = clawData.ClawStrechBackTime;
        
        //Add to list
        ExistingBalls.Add(go);
        //reset CD
        SpawnTimer = spawnerData.SpawnCD;
    }
}
