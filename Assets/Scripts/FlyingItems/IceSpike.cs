using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : Ball
{
    [Tooltip("刚被弹反开时的速度")]
    public float SpeedWhenCountered;
    [SerializeField] Color hostileColor;
    [SerializeField] Color friendlyColor;
    // Start is called before the first frame update
    protected override void HandleRotation()
    {
        Vector2 origin = new Vector3(0, -1, 0);
        Vector2 v = GetVelocity();
        if (Vector2.Angle(v, Gravity)>0)
            return;

        float angle = Vector2.Angle(origin, v);
        if (v.x < 0)
            angle = -angle;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public override void GetCountered(Transform playerTrans)
    {
        Friendly = true;
        Vector2 TargetDirection = transform.position - playerTrans.position;
        Gravity = (TargetDirection.normalized) * Gravity.y;
        SetVelocity(TargetDirection.normalized * SpeedWhenCountered);
    }
    public override void UpdateView()
    {
        if (isHostile)
        {
            mImg.color = hostileColor;
        }
        else
        {
            mImg.color = friendlyColor;
        }
    }
}
