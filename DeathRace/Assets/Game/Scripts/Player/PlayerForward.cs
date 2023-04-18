using PathCreation;
using System; 
using UniRx; 
using UnityEngine;

public class PlayerForward : MonoBehaviour {
    PlayerController _controller;

    public PathCreator PathCreator { get; private set; }
    IDisposable _pathControllerRX;
    float _distance;
    
    public void Initialize(PlayerController controller) {
        _controller = controller;
        PathCreator = GameObject.Find("Path").GetComponent<PathCreator>();
    }  
    public void Pause(bool value) { 
        _pathControllerRX?.Dispose();
        if (value)
            _pathControllerRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(PathRX);
    }
    void PathRX(long obj) {
        _distance += _controller.Stats.ActiveSpeed * Time.deltaTime;
        AllignToRoad();
        Accelerate();
        //Debug.Log(_controller.Stats.ActiveSpeed);
    }
    void Accelerate() => _controller.Stats.Accelerate(); 
    void AllignToRoad() {
        transform.position = PathCreator.path.GetPointAtDistance(_distance, EndOfPathInstruction.Loop);
        _controller.Parts.SetBody(PathCreator.path.GetRotationAtDistance(_distance, EndOfPathInstruction.Loop));
    }
}