using PathCreation;
using System;
using UniRx;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    PlayerForward _forward;
    PlayerSwipe _swipe;
    public PlayerForward Forward { get { return _forward; } }   
    public PlayerSwipe Swipe { get { return _swipe; } }   


    private void Start() {
        MainManager.Instance.Event.onGamePause += OnPause;
        MainManager.Instance.Event.onGamePause += OnUnPause;
        MainManager.Instance.Event.onGameStart += OnStart;

        _forward = GetComponent<PlayerForward>();

        _forward.Initialize();

        Pause(true);
    }
    public void OnStart() {
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
