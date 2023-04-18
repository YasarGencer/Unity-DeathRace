using UnityEngine;
using System;
using UniRx;
using PathCreation.Examples;

public abstract class APlayerComponent : MonoBehaviour {
    protected PlayerController _controller;

    IDisposable _updateRX;
    public virtual void Initialize(PlayerController controller) {
        MainManager.Instance.Event.onGamePause += OnPause;
        MainManager.Instance.Event.onGamePause += OnUnPause;
        MainManager.Instance.Event.onGameStart += OnStart;
        _controller = controller;

        Pause(true);
    } 
    public void Pause(bool value) {
        _updateRX?.Dispose();
        if (value)
            _updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
    }
    protected abstract void UpdateRX(long obj);
    public void OnStart() { 
        Pause(true);
    }
    public void OnPause() {
        Pause(false);
    }
    public void OnUnPause() {
        Pause(false);
    }
} 
