using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager
{
    public static LevelManager Instance;
    public Action OnLoadNextScene;
    private Scene curScene;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        Instance = new LevelManager();
        //DontDestroyOnLoad(Instance.gameObject);
    }

    public void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //�߽紦��
        if (nextLevelIndex > SceneManager.sceneCountInBuildSettings)
            nextLevelIndex = 0;

        SceneManager.LoadScene(nextLevelIndex);
        OnLoadNextScene?.Invoke();
    }
    public void LoadNextLevelAsync()
    {
        //Camera.main.GetComponent<AudioListener>().enabled = false;
        EventSystem.current.enabled = false;

        curScene = SceneManager.GetActiveScene();
        int nextLevelIndex =  curScene.buildIndex+ 1;
        //�߽紦��
        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings)
            nextLevelIndex = 0;

        AsyncOperation ao =  SceneManager.LoadSceneAsync(nextLevelIndex,LoadSceneMode.Additive);
        ao.completed += (a) =>
        {
            SceneManager.UnloadSceneAsync(curScene);
        };
        OnLoadNextScene?.Invoke();
        
    }
}
