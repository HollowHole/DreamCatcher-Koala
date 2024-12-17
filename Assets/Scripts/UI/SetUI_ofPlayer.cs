using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class SetUI_ofPlayer : MonoBehaviour
{
    public static SetUI_ofPlayer Instance { get; private set; }

    protected TextMeshProUGUI contText;
    

    public virtual void UpdateUI(int value){
        contText.text=value.ToString();
    }

    protected void Awake(){
        Instance=this;
        contText=transform.GetComponent<TextMeshProUGUI>();
    }

}
