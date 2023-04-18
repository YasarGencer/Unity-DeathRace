using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isTest;
    public static bool GamePause;
    public void Initialize() {
        GamePause = false;

        if (isTest)
            return;
        LoadScene(2);
    }
    public void OnStart() { 
    }
    public void OnPause() { 
    }
    public void OnUnPause() { 
    }
    public void LoadScene(int index) {
        MainManager.Instance.Event.RunOnGameLoad(); 
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }

    public void StartGame() {
        MainManager.Instance.Event.RunOnGameStart(); 
    }
}
