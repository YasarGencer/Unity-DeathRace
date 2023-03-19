 using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GamePause;
    public void Initialize() {
        GamePause = false;
         
        MainManager.Instance.Event.onGamePause += OnPause;
        MainManager.Instance.Event.onGamePause += OnUnPause;
        MainManager.Instance.Event.onGameStart += OnStart;

        MainManager.Instance.Event.RunOnGameStart();
    }
    public void OnStart() { 
    }
    public void OnPause() { 
    }
    public void OnUnPause() { 
    }
}
