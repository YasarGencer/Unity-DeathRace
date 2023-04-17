using PathCreation;
using System;
using UniRx;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] GameObject _car;
    IDisposable _swipeControllerRX, _pathControllerRX;
    [Header("PATH")]
    [SerializeField] PathCreation.PathCreator _pathCreator;
    [SerializeField] float _activeSpeed;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _roadWidth;
    float _distance;
    public EndOfPathInstruction endOfPathInstruction;

    [Header("SWIPE")]
    [SerializeField] float swipeSpeed;
    float swipeInfo;

    private void Start() {
        MainManager.Instance.Event.onGamePause += OnPause;
        MainManager.Instance.Event.onGamePause += OnUnPause;
        MainManager.Instance.Event.onGameStart += OnStart;

        _car.transform.localPosition = Vector3.zero;

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
        _swipeControllerRX?.Dispose();
        _pathControllerRX?.Dispose();
        if (value) {
            _swipeControllerRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(SwipeRX);
            _pathControllerRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(PathRX);
        }
    } 
    void PathRX(long obj) {
        _distance += _activeSpeed * Time.deltaTime;
        transform.position = _pathCreator.path.GetPointAtDistance(_distance, endOfPathInstruction);
        transform.rotation = _pathCreator.path.GetRotationAtDistance(_distance, endOfPathInstruction);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }
    void SwipeRX(long obj) {
        if (swipeInfo == 0)
            return;
        _car.transform.Translate(new Vector3(swipeInfo * Time.deltaTime * swipeSpeed, 0, 0));
        _car.transform.localPosition = new(Mathf.Clamp(_car.transform.localPosition.x, -_roadWidth, _roadWidth), _car.transform.localPosition.y, _car.transform.localPosition.z);
    }

    public void SwipeInfo(float value) {
        swipeInfo = value;
    }
}
