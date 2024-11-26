using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingUI : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        GameObject myPrefab = Resources.Load("Settings") as GameObject;
        GameObject instance = Instantiate(myPrefab);
        DontDestroyOnLoad(instance);
    }

    [SerializeField]GameObject panel;

    private void Start()
    {
        ResumeGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }
    public void PauseGame()
    {
        panel.SetActive (true);
        Time.timeScale = 0;
    }

}
