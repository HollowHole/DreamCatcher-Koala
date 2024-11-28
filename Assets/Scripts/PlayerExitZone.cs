using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExitZone : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //给玩家开无敌
            collision.GetComponent<PlayerController>().SetInvincibal(TransitionAnim.TransitionLastTime);

            FindObjectOfType<TransitionAnim>().PlayExitAnimAndSwitchScene();
        }

    }
}
