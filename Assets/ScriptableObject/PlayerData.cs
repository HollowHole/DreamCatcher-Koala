using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/玩家数据", order = 0)]
public class PlayerData : ScriptableObject
{
    [Tooltip("弹反半径")]
    public float CounterRadius = 5f;
    [Tooltip("弹反时长")]
    public float CounterLastTime = 0.5f;
    [Tooltip("无敌时长")]
    public float InvincibalTime = 3f;
    [Tooltip("弹反CD")]
    public float CounterCD = 1f;
    [Tooltip("初始下落速度")]
    public float FallingSpeedInit = 8f;
    [Tooltip("水平移动速度")]
    public float HorizontalSpeed = 2f;
    [Tooltip("下落速度改变速度")]
    public float VerticalSpeed = 0.5f;
    [Tooltip("最大下落速度")]
    public float MaxFallSpeed = 10;
    [Tooltip("最小下落速度")]
    public float MinFallSpeed = 4;
    [Tooltip("初始血量")]
    public int hpInit = 5;
}
