using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingUI : MonoBehaviour
{
    public static bool isGamePause;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        GameObject myPrefab = Resources.Load("Settings") as GameObject;
        GameObject instance = Instantiate(myPrefab);
        DontDestroyOnLoad(instance);

        isGamePause = false;
    }

    [SerializeField]GameObject panel;

    private void Start()
    {
        ResumeGame();
    }
    private void Update()
    {
        if (!GameoverMgr.isGameover&&Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.activeInHierarchy)
                ResumeGame();
            else
                PauseGame();
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        panel.SetActive(false);

        Time.timeScale = 1;

        isGamePause = false;
    }
    public void PauseGame()
    {
        panel.SetActive (true);

        Time.timeScale = 0;

        isGamePause = true;
    }

}
