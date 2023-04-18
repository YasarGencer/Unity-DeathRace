using PathCreation;
using System;
using UniRx;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] PlayerStats _stats;
    Forward _forward;
    Swipe _swipe;
    Parts _parts;
    public PlayerStats Stats { get { return _stats; } }   
    public Forward Forward { get { return _forward; } }   
    public Swipe Swipe { get { return _swipe; } }   
    public Parts Parts{ get { return _parts; } }   


    private void Start() {
        MainManager.Instance.Event.onGamePause += OnPause;
        MainManager.Instance.Event.onGamePause += OnUnPause;
        MainManager.Instance.Event.onGameStart += OnStart;

        _forward = GetComponent<Forward>();
        _swipe = GetComponent<Swipe>();
        _parts = GetComponent<Parts>();
          
    }
    public void OnStart() { 
        _forward.Initialize(this);
        _swipe.Initialize(this);
        _stats.Initialize();
        _parts.Initialize(this);

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
