using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetting : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool HardMode;
    private void Awake()
    {
        HardMode = false;
    }
    public void ChangeMode(bool value)
    {
        HardMode = value;
        Debug.Log("HardMode set " + value+"!");
    }
}
