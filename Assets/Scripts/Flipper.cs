using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    public float restAngle = 0f;
    public float pressedAngle = 45f;
    public float flipSpeed = 360f;
    public KeyCode inputKey;

    private HingeJoint hinge;
    private bool isFlipping;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        JointSpring spring = hinge.spring;
        spring.targetPosition = restAngle;
        hinge.spring = spring;
    }

    void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            isFlipping = true;
        }
        if (Input.GetKeyUp(inputKey))
        {
            isFlipping = false;
        }

        JointSpring spring = hinge.spring;
        spring.targetPosition = isFlipping ? pressedAngle : restAngle;
        spring.spring = flipSpeed;
        hinge.spring = spring;
    }
}