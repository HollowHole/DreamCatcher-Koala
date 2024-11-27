using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverMgr : MonoBehaviour
{
    public static bool isGameover;
    public static GameoverMgr Instance;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        GameObject myPrefab = Resources.Load("GameoverUI") as GameObject;
        Instance = Instantiate(myPrefab).GetComponent<GameoverMgr>();
        DontDestroyOnLoad(Instance.gameObject);
        
        Instance.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void Gameover()
    {
        Instance.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0;
        isGameover = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void TryAgain()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
        isGameover = false;
        Instance.transform.GetChild(0).gameObject.SetActive(false);
    }
    
}
