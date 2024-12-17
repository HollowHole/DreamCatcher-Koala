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
            transform.position = pos;

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
            PlayerOffset = (transform.position - player.position).y;
        }
    }
}
