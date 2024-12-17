using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;


public class SetBallNumUI : SetUI_ofPlayer
{
    public static SetBallNumUI Instance { get; private set; }

    // private TextMeshProUGUI contText;
    private int oriHp;

    public override void UpdateUI(int value){
        base.UpdateUI(value);
        oriHp=value;
    }

    void Awake(){
        Instance=this;
        contText=transform.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // PlayerController.Instance.UpdateBallNumUI(PlayerController.Instance.ballNum);
        oriHp=PlayerController.Instance.ballNum;
        UpdateUI(oriHp);
    }
}
