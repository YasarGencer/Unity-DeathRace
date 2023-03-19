using UnityEngine;

public class MainManager : MonoBehaviour
{
    bool _isInit = false;

    public static MainManager Instance;

    [SerializeField] private GameManager _game;
    [SerializeField] private EventManager _event;

    public GameManager Game { get { return _game; } }
    public EventManager Event { get { return _event; } }


    private void Awake() {
        Initialize();
    }
    void Initialize() {
        if (_isInit)
            return;
        _isInit= true;

        Instance = this;

        _event.Initialize(); 
        _game.Initialize();
    }
}
