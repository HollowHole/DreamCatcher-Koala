using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void TurnLevel1()
    {
        LevelManager.Instance.LoadNextLevelAsync();
    }
}