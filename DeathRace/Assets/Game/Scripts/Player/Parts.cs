using UnityEngine;

public class Parts : APlayerComponent {
    [SerializeField] Transform Body1, Body2;
    [SerializeField] Transform Wheels;
    [SerializeField] Transform WheelFL, WheelFR, WheelBL, WheelBR;
    public Transform ChildBody { get { return Body1; } }

    float currentWheelSideways = 0,currentBody2 = 0;
    float currentWheelForward;
    public override void Initialize(PlayerController controller) {
        base.Initialize(controller);
        currentWheelSideways = 0;
        currentWheelForward = 0;
        currentBody2 = 0;
    }
    protected override void UpdateRX(long obj) { 
        SetWheels();
    } 
    public void SetBody(Quaternion angles) {
        transform.rotation = angles; 
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }     
    public void SetSwipeRotation(float value) { 
        if (value == 0) {
            if (Body2.localRotation.y * 360 < -2)
                SwipeValue(0, 70);
            else if (Body2.localRotation.y * 360 > 2)
                SwipeValue(0, -70);
            if (WheelFL.localRotation.y * 360 < -1)
                SwipeValue(80, 0);
            else if (WheelFL.localRotation.y * 360 > 1)
                SwipeValue(-80, 0);
        }
        else if (value > 0) { 
            SwipeValue(80, 50);
            if (currentWheelSideways < -1)
                SwipeValue(0, 50);
        }
        else {
            SwipeValue(-80, -50);
            if (currentWheelSideways > 1)
                SwipeValue(0, -50);
        }
        currentWheelSideways = Mathf.Clamp(currentWheelSideways, -45, 45);
        currentBody2 = Mathf.Clamp(currentBody2, -20, 20);

        WheelFL.localRotation = Quaternion.Euler(currentWheelForward, currentWheelSideways, 0);
        WheelFR.localRotation = Quaternion.Euler(currentWheelForward, currentWheelSideways, 0); 
        Body2.localRotation = Quaternion.Euler(0, currentBody2, 0); 
    }
    void  SwipeValue(float wheel, float body) {
        currentWheelSideways += Time.deltaTime * wheel;
        currentBody2 += Time.deltaTime * body;
    }
    public void SetWheels() {
        Vector3 eulerRotationF = WheelFL.rotation.eulerAngles;
        Vector3 eulerRotationB = WheelBL.rotation.eulerAngles;
        WheelFL.rotation = Quaternion.Euler(currentWheelForward, eulerRotationF.y, eulerRotationF.z);
        WheelFR.rotation = Quaternion.Euler(currentWheelForward, eulerRotationF.y, eulerRotationF.z);
        WheelBL.rotation = Quaternion.Euler(currentWheelForward, eulerRotationB.y, eulerRotationB.z);
        WheelBR.rotation = Quaternion.Euler(currentWheelForward, eulerRotationB.y, eulerRotationB.z);

        var value = _controller.Stats.ActiveSpeed * _controller.Stats.ActiveSpeed *  40;
        currentWheelForward += Time.deltaTime * value;
    }
}
