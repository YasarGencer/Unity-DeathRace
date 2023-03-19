using UnityEngine; 

public class EventManager : MonoBehaviour {


    public delegate void OnGameStart();
    public event OnGameStart onGameStart;
    public delegate void OnGameLoad();
    public event OnGameLoad onGameLoad;

    public delegate void OnGamePuase();
    public event OnGamePuase onGamePause;
    public delegate void OnGameUnPuase();
    public event OnGameUnPuase onGameUnPause;


    public void Initialize() {
    } 
    public void RunOnGamePause() {
        onGamePause();
    }
    public void RunOnGameUnPuase() {
        onGameUnPause();
    }
    public void RunOnGameStart() {
        onGameStart();
    }
    public void RunOnGameLoad() {
        onGameLoad();
    }
}
