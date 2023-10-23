using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform camera;

    public float sensitivity;

    public float xAxis;
    public float yAxis;
    private float xRot;
    private float yRot;

    private void Start()
    {
        xRot = 0;
        yRot = 0;
    }

    private void FixedUpdate()
    {
        xAxis = Input.GetAxis("Mouse X") * sensitivity;
        yAxis = Input.GetAxis("Mouse Y") * sensitivity;
        
        xRot += xAxis;
        yRot += yAxis;

        transform.rotation = Quaternion.Euler(-yRot,xRot,0);
    }

    private void OnDisable()
    {
        xRot = 0;
        yRot = 0;
    }
}
