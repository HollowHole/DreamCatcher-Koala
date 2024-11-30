using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnerData", menuName = "ScriptableObject/生成器数据", order = 0)]
public class SpawnerData : ScriptableObject
{
    [Tooltip("场景中该对象最大存在数量")]
    public int MaxBallCount = 5;
    [Tooltip("最小生成间隔")]
    public float SpawnCD = 2f;
    [Tooltip("生成位置的左右最大偏移量")]
    public float SpawnPointOffsetH;
    [Tooltip("生成时最小y轴速度")]
    public float minVyOffset;
    [Tooltip("生成时最大y轴速度")]
    public float maxVyOffset;
    [Tooltip("生成时最大x轴速度")]
    public float vxRange;
    
    [Tooltip("生成物体受到的最小重力")]
    public float minGravity;
    [Tooltip("生成物体受到的最大重力")]
    public float maxGravity;
}
