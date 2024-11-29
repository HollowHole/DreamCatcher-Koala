using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SplitBall : Ball
{
    [SerializeField] Sprite HostileImage;
    [SerializeField] Sprite FriendlyImage;
    [SerializeField] RuntimeAnimatorController HostileAnimCrl;
    [SerializeField] RuntimeAnimatorController FriendlyAnimCrl;

    public GameObject SpiltBallPre;
    public int childNum=3;

    static List<GameObject> BallList;

    protected override void Awake(){
        if(BallList==null){
            BallList=new List<GameObject>();
        }
        base.Awake();
        BallList.Add(this.gameObject);
    }
    public override void UpdateView()
    {
        if (isHostile)
        {
            mImg.sprite = HostileImage;
            animator.runtimeAnimatorController = HostileAnimCrl;
        }
        else
        {
            mImg.sprite = FriendlyImage;
            animator.runtimeAnimatorController = FriendlyAnimCrl;
        }
    }


    public void Spiliting(){
        Transform trans=this.transform; 

        Vector3 scaleGlobal=trans.lossyScale;
        Vector3 scaleLocal=trans.localScale;

        if(scaleGlobal.x< 0.5f) return;
        scaleLocal/=2f;
        // trans.localScale=scaleLocal;

        for(int i=0;i<childNum;i++){
            Debug.Log(i+":child");
            GameObject child=Instantiate(SpiltBallPre) as GameObject;
            child.transform.position=trans.position;
            child.transform.localScale=scaleLocal;
            child.transform.parent=GameObject.Find("ChildBalls").transform;
            child.tag="ChildBall";
            BallList.Add(child);
        }


        // Destroy(this.gameObject);
    }


}
