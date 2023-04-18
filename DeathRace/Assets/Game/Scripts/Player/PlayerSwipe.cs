using PathCreation;
using PathCreation.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerSwipe : MonoBehaviour {
    PlayerController _controller;
    IDisposable _swipeControllerRX;
    float _swipeInfo;
    float _roadWidth;
    public void Initialize(PlayerController controller) {
        _controller = controller;
        _roadWidth = GameObject.Find("Path").GetComponent<RoadMeshCreator>().roadWidth - .5f;
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
        AllignToSwipe();
        if (_swipeInfo == 0)
            return;
        Swipe();
        Brakes();
    }
    void AllignToSwipe() => _controller.Parts.SetSwipeRotation(_swipeInfo);
    void Swipe() {
        _controller.Parts.ChildBody.Translate(new Vector3(_swipeInfo * Time.deltaTime * _controller.Stats.SwipeSpeed, 0, 0));
        _controller.Parts.ChildBody.localPosition =
            new(Mathf.Clamp(_controller.Parts.ChildBody.localPosition.x, -_roadWidth, _roadWidth),
            _controller.Parts.ChildBody.localPosition.y, _controller.Parts.ChildBody.localPosition.z);  
    }
    void Brakes() => _controller.Stats.Brakes();


}
    