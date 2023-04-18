using PathCreation;
using System; 
using UniRx; 
using UnityEngine;

public class PlayerForward : MonoBehaviour {

    [SerializeField] PathCreation.PathCreator _pathCreator;
    [SerializeField] float _activeSpeed;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _roadWidth;

    IDisposable _pathControllerRX;
    float _distance;
    public EndOfPathInstruction endOfPathInstruction;
    public void Initialize() { 
    }  
    public void Pause(bool value) { 
        _pathControllerRX?.Dispose();
        if (value)
            _pathControllerRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(PathRX);
    }
    void PathRX(long obj) {
        _distance += _activeSpeed * Time.deltaTime;
        transform.position = _pathCreator.path.GetPointAtDistance(_distance, endOfPathInstruction);
        transform.rotation = _pathCreator.path.GetRotationAtDistance(_distance, endOfPathInstruction);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }
}