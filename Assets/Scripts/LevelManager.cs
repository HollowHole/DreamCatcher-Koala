using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    // Start is called before the first frame update
    public static LevelManager Instance;
    public Action OnLoadNextScene;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        Instance = new LevelManager();
        //DontDestroyOnLoad(Instance.gameObject);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        OnLoadNextScene?.Invoke();
    }
    public void LoadNextLevelAsync()
    {
        Debug.LogWarning("Async Function has not been Completed yet!");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        OnLoadNextScene?.Invoke();

    }

}
