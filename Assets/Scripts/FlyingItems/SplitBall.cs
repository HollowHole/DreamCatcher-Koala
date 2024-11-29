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
    public int childNum=2;

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
        Vector3 oriV=rb.velocity;

        Debug.Log(oriV);

        Vector3 scaleGlob=trans.lossyScale;
        Vector3 scaleLoc=trans.localScale;

        if(scaleGlob.x< 0.2f){
            Destroy(this.gameObject);
            return;
        }
        scaleLoc/=2f;



        List<GameObject> smaller=new List<GameObject>();

        for(int i=0;i<childNum;i++){
            // Debug.Log(i+":child");
            GameObject child=Instantiate(SpiltBallPre) as GameObject;
            smaller.Add(child);
        }
        
        smaller.Add(this.gameObject);
        foreach(GameObject child in smaller){
            child.transform.position=trans.position;
            child.transform.localScale=scaleLoc;
            child.transform.parent=GameObject.Find("ChildBalls").transform;
            child.tag="ChildBall";
            Vector3 changeV=new Vector3(UnityEngine.Random.Range(1f, 2f)*(-oriV.x/(Math.Abs(oriV.x)+0.001f))*2f, UnityEngine.Random.Range(0f, 0.5f), 0f);
            child.GetComponent<Rigidbody2D>().velocity=(oriV+changeV);
        }

    }

    public void SetGravity(float gravity){}

    public void SetGravity(Vector2 gravity){}



}
