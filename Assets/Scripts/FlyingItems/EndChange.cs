using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChange : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer=0f;


    private bool changeIf=false;

    public float y0=-5f;
    public float z0=12f;

    public float yEnd=0f;
    public float zEnd=21f;

    void Awake(){
        Vector3 pos=transform.position;
        transform.position=new Vector3(pos.x, y0, z0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!changeIf){
            timer+=Time.deltaTime;
            // Debug.Log(timer);
            if(timer>=2.8){
                Vector3 pos=transform.position;
                transform.position=new Vector3(pos.x, pos.y+(yEnd-y0)*Time.deltaTime, pos.z+(zEnd-z0)*Time.deltaTime);
                // Debug.Log(transform.position);
                if(transform.position.y>=yEnd){
                    changeIf=true;
                }
            }
        }

    }
}
