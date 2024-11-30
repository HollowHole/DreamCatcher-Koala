using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ClawSpawnerData", menuName = "ScriptableObject/爪子生成器数据", order = 0)]
public class ClawSpawnerData : SpawnerData
{
    [Header("爪子数据")]
    [Tooltip("爪子伸出需要的时间")]
    public float ClawStrechOutTime = 0.5f;
    [Tooltip("爪子伸回需要的时间")]
    public float ClawStrechBackTime = 2f;
    [Tooltip("预警时间")]
    public float AlertTime = 3f;
    [Tooltip("爪子刷新点高度离玩家高度的最小值")]
    public float SpawnOffsetVerticalMin;
    [Tooltip("爪子刷新点高度离玩家高度的最大值")]
    public float SpawnOffsetVerticalMax;
    [Tooltip("爪子长度最小值")]
    public float minLength;
    [Tooltip("爪子长度最大值")]
    public float maxLength;

    
}
