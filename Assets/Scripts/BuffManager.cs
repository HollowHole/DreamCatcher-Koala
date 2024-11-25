using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    private List<Buff> BuffList;
    PlayerController player;
    private void Awake()
    {
        BuffList = new List<Buff>();
        player = GetComponent<PlayerController>();
    }
    public void AddBuff(Buff buff)
    {
        foreach(Buff b in BuffList) 
        {
            if(buff.mName == b.mName)
            {
                if (buff.mName == Buff.BuffName.Decelerate)
                {
                    BuffList.Remove(b);
                    BuffList.Add(new Buff(Buff.BuffName.Freeze, 3f));//TODO
                }
                else
                {
                    b.LastTime = buff.LastTime;
                }
            }
            //End function
            return; 
        }
        BuffList.Add(buff);
    }
    public void HandleBuffEffect()
    {

        BuffTakeEffect(player);
    }
    public void BuffTakeEffect(PlayerController _player)
    {
        List<Buff> newList = new List<Buff>();
        foreach (Buff buff in BuffList)
        {
            if (Input.GetMouseButtonDown(0) && buff.mName == Buff.BuffName.Freeze)
            {
                buff.LastTime -= 0.1f;
            }
            buff.TakeEffect(_player);
            if (buff.LastTime > 0) 
            {
                newList.Add(buff);
            }
        }
        BuffList = newList;
    }
}
public class Buff
{
    public enum BuffName { Decelerate,Freeze}

    public BuffName mName;
    public float LastTime;
    public Buff(BuffName _name, float _lastTime)
    {
        mName = _name;
        LastTime = _lastTime;
    }
    public void TakeEffect(PlayerController player)
    {
        LastTime -=Time.deltaTime;
        switch (mName)
        {
            case BuffName.Decelerate:
                player.ChangeSpeedHor(0.66f);
                break;
            case BuffName.Freeze:
                player.ChangeSpeedHor(0);
                break;
            default:
                Debug.Log("Undefined Buff!");
                break;
        }
    }
}