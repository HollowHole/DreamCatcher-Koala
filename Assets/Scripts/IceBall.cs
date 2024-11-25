using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : Fireballs
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.GetComponent<BuffManager>().AddBuff(new Buff(Buff.BuffName.Decelerate, 3f));
        }
    }
}
