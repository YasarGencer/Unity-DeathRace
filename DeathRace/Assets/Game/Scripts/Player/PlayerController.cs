using PathCreation;
using System;
using UniRx;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] PlayerStats _stats;
    PlayerForward _forward;
    PlayerSwipe _swipe;
    PlayerParts _parts;
    public PlayerStats Stats { get { return _stats; } }   
    public PlayerForward Forward { get { return _forward; } }   
    public PlayerSwipe Swipe { get { return _swipe; } }   
    public PlayerParts Parts{ get { return _parts; } }   


    private void Start() {
        MainManager.Instance.Event.onGamePause += OnPause;
        MainManager.Instance.Event.onGamePause += OnUnPause;
        MainManager.Instance.Event.onGameStart += OnStart;

        _forward = GetComponent<PlayerForward>();
        _swipe = GetComponent<PlayerSwipe>();
        _parts = GetComponent<PlayerParts>();
          
    }
    public void OnStart() { 
        _forward.Initialize(this);
        _swipe.Initialize(this);
        _stats.Initialize();
        _parts.Initialize();

        Pause(true);
    }
    public void OnPause() {
        Pause(false);
    }
    public void OnUnPause() {
        Pause(false);
    }
    void Pause(bool value) {
        _forward.Pause(value);
        _swipe.Pause(value);
    } 
}
