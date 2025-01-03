using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    float PlayerOffset;
    Transform player;
    
    bool isFollowingPlayer;

    static private float scP=0f;

    [SerializeField]
    private float scrollSpeed;
    [SerializeField]
    private float minAngle; // 相机俯仰角的最小值
    [SerializeField]
    private float maxAngle; // 相机俯仰角的最大值
    [SerializeField]
    private float orbitRadius;
    [SerializeField]
    private float lastAngle;
    [SerializeField]
    private float oriZ;

    public float score{
        get{
            // Debug.Log("Get_ballNum: "+ballN);
            return scP;
        }
        set{
            // Debug.Log("Set_ballNum: "+ballN);
            scP=(value>=0?value:0); 
        }
    }

    void Awake(){
        scrollSpeed = 100f;
        minAngle = -30f;
        maxAngle = 30f;
        lastAngle=0f;
        oriZ=-10f;
    }
    private void Start()
    {
        isFollowingPlayer = false;
        player = PlayerController.Instance.transform;
    }
    private void Update()
    {
        if(SetUI_ofPlayer.Instance!=null)
            SetUI_ofPlayer.Instance.UpdateUI(((int)score));

        if (isFollowingPlayer)
        {
            Vector3 pos = transform.position;
            pos.y = player.position.y + PlayerOffset;
            pos.z=oriZ;
            transform.position = pos;

            Vector3 rCenter=player.position+new Vector3(0f,PlayerOffset,0f);

            transform.rotation=Quaternion.identity;
            RollCamera(rCenter);

            score+=Time.deltaTime;
        }
    }
    
    public void FollowPlayer(bool flag)
    {
        if (isFollowingPlayer == flag)
        {
            return;
        }
        isFollowingPlayer=flag;
        if(flag)
        {
            Vector3 temp=(transform.position - player.position);
            PlayerOffset = temp.y;
            orbitRadius=temp.magnitude;
        }
    }

    void RollCamera(Vector3 rCenter){
        Vector3 axisTemp=new Vector3(1,0,0);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {

            // transform.position = playerPos + transform.forward * orbitRadius;
            // transform.LookAt(playerPos); // 确保相机面向原点
            // 计算相机应该旋转的角度
            float angleNew = scroll * scrollSpeed;
            float angleChange = Mathf.Clamp(lastAngle+angleNew, minAngle, maxAngle);
            Debug.Log(angleChange);

            transform.RotateAround(rCenter, axisTemp, angleChange);
            lastAngle=angleChange;
        }
        else{
            transform.RotateAround(rCenter, axisTemp, lastAngle);
        }
    }
}
