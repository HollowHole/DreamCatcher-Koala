using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartingManager : MonoBehaviour
{
    [SerializeField] GameObject InputTip;
    private IEnumerator Start()
    {
        InputTip.SetActive(false);

        while (true)
        {
            if (!Input.GetMouseButtonDown(0))
                break;
            yield return null;
        }

        //开始游戏

        yield return new WaitForSeconds(3f);

        InputTip.SetActive(true);

    }
    public void CloseInputTipPanel()
    {
        LevelManager.Instance.LoadNextLevelAsync();
        InputTip.SetActive(false);
    }
}
