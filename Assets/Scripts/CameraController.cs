using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    float PlayerOffset;
    Transform player;
    
    bool isFollowingPlayer;
    private void Start()
    {
        isFollowingPlayer = false;
        player = PlayerController.Instance.transform;
    }
    private void Update()
    {
        if (isFollowingPlayer)
        {
            Vector3 pos = transform.position;
            pos.y = player.position.y + PlayerOffset;
            transform.position = pos;
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
