using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class TransitionAnim : MonoBehaviour
{
    public static float TransitionLastTime = 2f;
    [SerializeField] Camera transitionCam;
    [SerializeField] Camera mainCam;
    // Start is called before the first frame update
    Image image;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        transitionCam = transform.GetChild(0).GetComponent<Camera>();
    }
    private void Start()
    {
        PlayStartSceneAnim();
    }
    public void PlayExitAnimAndSwitchScene()//Will try unload scene in this function
    {
        //image.fillAmount = 0;
        //Tweener tweener = image.DOFillAmount(1, TransitionLastTime);
        Tweener tweener;
        transitionCam.gameObject.SetActive(true);
        transitionCam.orthographicSize = 0.1f;

        transitionCam.transform.position = PlayerController.Instance.transform.position;
        transitionCam.transform.position += new Vector3(0, 0, -5);
        tweener = transitionCam.DOOrthoSize(mainCam.orthographicSize, TransitionLastTime).From();
        tweener.SetUpdate(true);
        tweener =  transitionCam.transform.DOMove(mainCam.transform.position, TransitionLastTime).From();

        tweener.SetUpdate(true);
        tweener.SetAutoKill(false);
        StartCoroutine(WaitAnimEndSwitchScene(tweener));
    }
    void PlayStartSceneAnim()//Then Start Game in this function
    {
        //image.fillAmount = 1;
        //Tweener tweener = image.DOFillAmount(0, TransitionLastTime);
        Tweener tweener;
        transitionCam.gameObject.SetActive(true);
        transitionCam.transform.position = PlayerController.Instance.transform.position;
        transitionCam.transform.position += new Vector3(0, 0, -5);
        tweener = transitionCam.DOOrthoSize(mainCam.orthographicSize, TransitionLastTime);
        tweener.SetUpdate(true);
        tweener = transitionCam.transform.DOMove(mainCam.transform.position, TransitionLastTime);

        tweener.SetUpdate(true);
        tweener.SetAutoKill(false);
        StartCoroutine(WaitAnimEndPlayerStartGame(tweener));
    }

    private void OnDestroy()
    {
        //Debug.Log("listener disposed");
    }
    IEnumerator WaitAnimEndPlayerStartGame(Tweener tweener)
    {
        //float playerFallingSpeed = PlayerController.Instance.FallingSpeed;
        //PlayerController.Instance.FallingSpeed = 0;
        Time.timeScale = 0;

        while (!tweener.IsComplete())
        {
            yield return null;
        }

        Debug.Log("Start Game");

        if(!SettingUI.isGamePause)
            Time.timeScale = 1;

        transitionCam .gameObject.SetActive(false);
        //PlayerController.Instance.FallingSpeed = playerFallingSpeed;
    }
    IEnumerator WaitAnimEndSwitchScene(Tweener tweener)
    {
        Time.timeScale = 0;

        while (!tweener.IsComplete())
        {
            yield return null;
        }
        if (!SettingUI.isGamePause)
            Time.timeScale = 1;
        LevelManager.Instance.LoadNextLevelAsync();

        transitionCam.gameObject.SetActive(false);
    }
}
