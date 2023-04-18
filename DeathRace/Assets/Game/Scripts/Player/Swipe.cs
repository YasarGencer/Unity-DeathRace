using PathCreation.Examples;
using System; 
using UniRx;
using UnityEngine; 

public class Swipe : APlayerComponent {
    float _swipeInfo;
    float _roadWidth;
    public override void Initialize(PlayerController controller) {
        base.Initialize(controller);
        _roadWidth = GameObject.Find("Path").GetComponent<RoadMeshCreator>().roadWidth - .5f;
    }
    protected override void UpdateRX(long obj) {
        AllignToSwipe();
        if (_swipeInfo == 0)
            return;
        OnSwipe();
        Brakes();
    }

    public void SwipeInfo(float value) {
        _swipeInfo = value;
    }
    void AllignToSwipe() => _controller.Parts.SetSwipeRotation(_swipeInfo);
    void OnSwipe() {
        _controller.Parts.ChildBody.Translate(new Vector3(_swipeInfo * Time.deltaTime * _controller.Stats.SwipeSpeed, 0, 0));
        _controller.Parts.ChildBody.localPosition =
            new(Mathf.Clamp(_controller.Parts.ChildBody.localPosition.x, -_roadWidth, _roadWidth),
            _controller.Parts.ChildBody.localPosition.y, _controller.Parts.ChildBody.localPosition.z);  
    }
    void Brakes() => _controller.Stats.Brakes();


}
    