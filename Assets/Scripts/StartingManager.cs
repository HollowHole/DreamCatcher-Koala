using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartingManager : MonoBehaviour
{
    [SerializeField] GameObject InputTip;
    [SerializeField] GameObject Koala1;
    [SerializeField] GameObject Koala2;
    [SerializeField] Transform transitionCam;
    [SerializeField] float transitionTime = 2f;
    [SerializeField] GameObject ExitButton;
    private IEnumerator Start()
    {
        InputTip.SetActive(false);
        transitionCam.gameObject.SetActive(false);
        Koala1.SetActive(true);
        Koala2.SetActive(false);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
                break;
            yield return null;
        }

        //开始游戏
        Koala1.SetActive(false);
        Koala2.SetActive(true);
        yield return new WaitForSeconds(7.833f);

        InputTip.SetActive(true);
        ExitButton.SetActive(false);

    }
    public void CloseInputTipPanel()
    {
        StartCoroutine(LoadLevel1());
    }
    IEnumerator LoadLevel1()
    {
        
        InputTip.SetActive(false);
        transitionCam.gameObject.SetActive(true);
        transitionCam.GetComponent<Camera>().orthographicSize = 0.1f;
        transitionCam.GetComponent<Camera>().DOOrthoSize(Camera.main.orthographicSize, transitionTime).From();
        transitionCam.DOMove(Camera.main.transform.position, transitionTime).From();

        yield return new WaitForSeconds(transitionTime);
        LevelManager.Instance.LoadNextLevelAsync();
    }
}
