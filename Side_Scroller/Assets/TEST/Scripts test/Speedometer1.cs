using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour {

    private const float MAX_SPEED_ANGLE = -20;
    private const float ZERO_SPEED_ANGLE = 230;

    private Transform needleTranform;

    private float speedMax;
    private float speed;


    private void Awake() {
        needleTranform = transform.Find("needle");

        speed = 0f;
        speedMax = 200f;
    }

    private void Update() {

        //speed += 30f * Time.deltaTime;
        //if (speed > speedMax) speed = speedMax;


        needleTranform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }

    private float GetSpeedRotation() {
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

        float speedNormalized = speed / speedMax;

        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;
    }
}
