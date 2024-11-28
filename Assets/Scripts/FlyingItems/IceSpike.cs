using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : Ball
{
    [SerializeField] Color hostileColor;
    [SerializeField] Color friendlyColor;
    // Start is called before the first frame update
    protected override void HandleRotation()
    {
        //Do nothing
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
