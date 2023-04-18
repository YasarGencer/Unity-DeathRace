using PathCreation;
using System; 
using UniRx; 
using UnityEngine;

public class Forward : APlayerComponent { 

    public PathCreator PathCreator { get; private set; } 
    float _distance;
    
    public override void Initialize(PlayerController controller) {
        base.Initialize(controller);
        PathCreator = GameObject.Find("Path").GetComponent<PathCreator>();
    }
    protected override void UpdateRX(long obj) {
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