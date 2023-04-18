using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour {

    IDisposable _swipeControllerRX;
    float _swipeInfo;
    Transform _car;
    [SerializeField] float _roadWidth;
    [SerializeField] float _swipeSpeed;
    public void Initialize() {
        _car = gameObject.transform.GetChild(0);
    }
    public void Pause(bool value) {
        _swipeControllerRX?.Dispose();
        if (value)
            _swipeControllerRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(SwipeRX);
    }
    public void SwipeInfo(float value) {
        _swipeInfo = value;
    }
    void SwipeRX(long obj) {
        if (_swipeInfo == 0)
            return;
        _car.Translate(new Vector3(_swipeInfo * Time.deltaTime * _swipeSpeed, 0, 0));
        _car.localPosition = new(Mathf.Clamp(_car.localPosition.x, -_roadWidth, _roadWidth), _car.localPosition.y, _car.localPosition.z);
    }

}
