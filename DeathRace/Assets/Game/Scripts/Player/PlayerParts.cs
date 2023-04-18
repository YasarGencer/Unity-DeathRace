using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParts : MonoBehaviour
{
    [SerializeField] Transform Body1, Body2;
    [SerializeField] Transform Wheels;
    [SerializeField] Transform WheelFL, WheelFR, WheelBL, WheelBR;
    public Transform ChildBody { get { return Body1; } }

    float currentWheels = 0,currentBody2 = 0;
    public void Initialize() {
        currentWheels = 0;
        currentBody2 = 0;
    }
    public void SetBody(Quaternion angles) {
        transform.rotation = angles;
        //set z to 0
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }     
    public void SetSwipeRotation(float value) {

        if(value == 0) {
            Debug.Log("GO STRAIGHT");
            if (currentWheels < 0) {
                currentWheels += Time.deltaTime * 50;
                currentBody2 += Time.deltaTime * 20;
            } else if (currentWheels > 0) {
                currentWheels -= Time.deltaTime * 50;
                currentBody2 -= Time.deltaTime * 20;
            }
        } else if (value > 0) {
            Debug.Log("TURN RIGHT");
            currentWheels += Time.deltaTime * 50;
            currentBody2 += Time.deltaTime * 20;
        } else {
            Debug.Log("TURN LEFT");
            currentWheels -= Time.deltaTime * 50;
            currentBody2 -= Time.deltaTime * 20;
        } 

        currentWheels = Mathf.Clamp(currentWheels, -25, 25);
        currentBody2 = Mathf.Clamp(currentBody2, -20, 20);

        WheelFL.localRotation = Quaternion.Euler(0, currentWheels, 0);
        WheelFR.localRotation = Quaternion.Euler(0, currentWheels, 0); 
        Body2.localRotation = Quaternion.Euler(0, currentBody2, 0); 
    }
}
