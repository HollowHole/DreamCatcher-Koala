using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;


public class SetBallNumUI : MonoBehaviour
{
    public static SetBallNumUI Instance { get; private set; }

    private TextMeshProUGUI ballNumText;
    private int oriHp;

    public void UpdateUI(int value){
        ballNumText.text=value.ToString();
        oriHp=value;
    }

    // Start is called before the first frame update
    void Awake(){
        Instance=this;
        ballNumText=transform.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        // PlayerController.Instance.UpdateBallNumUI(PlayerController.Instance.ballNum);
        oriHp=PlayerController.Instance.ballNum;
        UpdateUI(oriHp);
    }
}
