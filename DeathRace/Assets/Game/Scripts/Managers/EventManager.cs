using Unity.VisualScripting;
using UnityEngine; 

public class EventManager : MonoBehaviour {


    public delegate void OnGameStart(); 

    public delegate void OnGamePuase();
    public delegate void OnGameUnPuase(); 

    public event OnGameStart onGameStart; 

    public event OnGamePuase onGamePause;
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
        onGameStart = null;
        onGamePause = null;
        onGameUnPause = null;
    }
}
