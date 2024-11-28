using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : Ball
{
    [SerializeField] Color hostileColor;
    [SerializeField]Color friendlyColor;

    protected override void HitPlayer(PlayerController player)
    {
        Destroy(gameObject);
        player.GetComponent<BuffManager>().AddBuff(new Buff(Buff.BuffName.Decelerate, 3f));
    }
    
    // Start is called before the first frame update
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
