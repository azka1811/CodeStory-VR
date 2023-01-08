using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRCamera : MonoBehaviour
{
 [SerializeField]
float dragRate;
[SerializeField]
Transform cameraTransform;

Quaternion initialRotation;
Quaternion attitude;
Vector2 dragDegrees;

void Awake()
{
    initialRotation = cameraTransform.rotation;
}

void Update()
{
    if (Input.touchCount == 1)
    {
        var touch = Input.GetTouch(0);
        dragDegrees.x += touch.deltaPosition.y * dragRate;
        dragDegrees.y += touch.deltaPosition.x * dragRate;
    }
    attitude = initialRotation * Quaternion.Euler(dragDegrees.x, 0, 0);
    cameraTransform.rotation = Quaternion.Euler(0, -dragDegrees.y, 0) * attitude;
}
}
