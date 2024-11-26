using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    [SerializeField] Image FrozenProgressFrame;
    [SerializeField] Image FrozenProgressFiller;
    [Tooltip("冰冻时长")]
    public float FrozenBuffLastTime = 3f;
    
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
                    BuffList.Add(new Buff(Buff.BuffName.Freeze, FrozenBuffLastTime));//TODO:冰冻时长
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
    public void SetFrozenProgress(float lastTime)
    {
        if(lastTime > 0)
            FrozenProgressFrame.enabled = true;
        else
            FrozenProgressFrame.enabled = false;
        FrozenProgressFiller.fillAmount = lastTime / FrozenBuffLastTime;
    }
    public void BuffTakeEffect(PlayerController _player)
    {
        //reset frozenProgressBar
        SetFrozenProgress(0f);

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
                player.GetComponent<BuffManager>().SetFrozenProgress(LastTime);
                break;
            default:
                Debug.Log("Undefined Buff!");
                break;
        }
    }
}