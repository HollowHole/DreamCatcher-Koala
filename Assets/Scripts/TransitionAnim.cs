using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TransitionAnim : MonoBehaviour
{
    public static float TransitionLastTime = 2f;
    // Start is called before the first frame update
    Image image;
    
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
        PlayStartSceneAnim();
    }
    public void PlayExitAnimAndSwitchScene()//Will try unload scene in this function
    {
        image.fillAmount = 0;
        Tweener tweener = image.DOFillAmount(1, TransitionLastTime);
        tweener.SetUpdate(true);
        tweener.SetAutoKill(false);
        StartCoroutine(WaitAnimEndSwitchScene(tweener));
    }
    void PlayStartSceneAnim()//Then Start Game in this function
    {
        image.fillAmount = 1;
        Tweener tweener = image.DOFillAmount(0, TransitionLastTime);
        tweener.SetUpdate(true);
        StartCoroutine(WaitAnimEndPlayerStartGame());
    }

    private void OnDestroy()
    {
        //Debug.Log("listener disposed");
    }
    IEnumerator WaitAnimEndPlayerStartGame()
    {
        //float playerFallingSpeed = PlayerController.Instance.FallingSpeed;
        //PlayerController.Instance.FallingSpeed = 0;
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(TransitionAnim.TransitionLastTime);

        Debug.Log("Start Game");
        Time.timeScale = 1;

        //PlayerController.Instance.FallingSpeed = playerFallingSpeed;
    }
    IEnumerator WaitAnimEndSwitchScene(Tweener tweener)
    {
        while (!tweener.IsComplete())
        {
            yield return null;
        }

        LevelManager.Instance.LoadNextLevelAsync();
    }
}
