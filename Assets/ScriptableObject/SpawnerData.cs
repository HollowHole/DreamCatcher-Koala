using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnerData", menuName = "ScriptableObject/����������", order = 0)]
public class SpawnerData : ScriptableObject
{
    [Tooltip("�����иö�������������")]
    public int MaxBallCount = 5;
    [Tooltip("����λ�õ��������ƫ����")]
    public float SpawnPointOffsetH;
    [Tooltip("����ʱ��Сy���ٶ�")]
    public float minVyOffset;
    [Tooltip("����ʱ���y���ٶ�")]
    public float maxVyOffset;
    [Tooltip("����ʱ���x���ٶ�")]
    public float vxRange;
    [Tooltip("��С���ɼ��")]
    public float SpawnCD = 2f;
    [Tooltip("���������ܵ�����С����")]
    public float minGravity;
    [Tooltip("���������ܵ����������")]
    public float maxGravity;
}
