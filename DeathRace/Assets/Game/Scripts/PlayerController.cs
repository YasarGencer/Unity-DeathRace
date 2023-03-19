using PathCreation;
using System; 
using UniRx;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _car;
    IDisposable _swipeControllerRX, _pathControllerRX;
    [Header("SWIPE")]
    [SerializeField] float _swipeSpeed;
    Vector3? _oldSwipePos;
    int _goRight;
    bool _go;
    Touch _touch;
    Vector2 _touchDown, _touchUp;
    [Header("PATH")]
    [SerializeField] PathCreation.PathCreator _pathCreator;
    [SerializeField] float _activeSpeed;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _roadWidth;
    float _distance;
    public EndOfPathInstruction endOfPathInstruction;

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

    void SwipeRX(long obj) {
        if (Input.touchCount > 0) { 
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began) {
                _go = true;
                _touchDown = _touch.position;    
                _touchUp = _touch.position;
            }
        }
        if (_go) {
            if (_touchDown.x > _touch.position.x)
                _goRight = -1;
            else if (_touchDown.x < _touch.position.x)
                _goRight = 1;

            if (_touch.phase == TouchPhase.Moved)
                _touchDown = _touch.position; 
            if (_touch.phase == TouchPhase.Stationary)
                _touchUp = _touchDown; 
            if (_touch.phase == TouchPhase.Ended) {
                _touchDown = _touch.position;
                _go = false;
            }
        }

        var go = _go ? _swipeSpeed * Time.deltaTime * _goRight : 0;
        _car.transform.Translate(new(go, 0, 0));
        _car.transform.localPosition = new(Mathf.Clamp(_car.transform.localPosition.x + go, -_roadWidth, _roadWidth), _car.transform.localPosition.y, _car.transform.localPosition.z);
    } 
    void PathRX(long obj) {
        _distance += _activeSpeed * Time.deltaTime;
        transform.position = _pathCreator.path.GetPointAtDistance(_distance, endOfPathInstruction);
        transform.rotation = _pathCreator.path.GetRotationAtDistance(_distance, endOfPathInstruction);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0); 
    }
}
