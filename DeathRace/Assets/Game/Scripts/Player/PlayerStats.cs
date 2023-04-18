using UnityEngine; 


[CreateAssetMenu(fileName = "New Car", menuName = "ScriptableObjects/Car")]
public class PlayerStats : ScriptableObject {
    [Header("FORWARD")] 
    public float Acceleration;
    public float BrakePower;
    public float MaxSpeed;
    public float ActiveSpeed { get; private set; }
    [Header("SWIPE")]
    public float SwipeSpeed;
    public void Initialize() {
        ActiveSpeed = 0;
    } 
    public void Accelerate() {
        ActiveSpeed += Time.deltaTime * Acceleration;
        FixSpeed();
    }
    public void Brakes() { 
        ActiveSpeed -= Time.deltaTime * (BrakePower + Acceleration);
        FixSpeed(MaxSpeed / 2);
    }
    void FixSpeed(float value = 0) {
        ActiveSpeed = Mathf.Clamp(ActiveSpeed, value, MaxSpeed);
    }
}
