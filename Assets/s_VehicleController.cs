using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class s_VehicleController : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 CoM;
    public float mph;

    private float throttle;
    private float steering;

    public float driveForce;
    public float acceleration;

    public float turnForce;

    private float wheelAngle;
    public float maxWheelAngle;
    public float wheelTurnTime;

    public float groundCheckLength;
    public LayerMask ground;

    public GameObject[] driveWheels;

  

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = CoM;
    }

    private void FixedUpdate()
    {
        throttle = Input.GetAxis("Vertical");
        steering = Input.GetAxis("Horizontal");
        mph = Mathf.Round(rb.velocity.magnitude * 2.24f);
      

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckLength, ground))
        {
            driveForce += throttle * acceleration;
            rb.AddRelativeForce(Vector3.forward * driveForce);


            if (mph < 10)
            {
                turnForce = mph * 1000;
            }
            else
            {
                turnForce = mph * 1000 / (mph / 5);
            }

            rb.AddRelativeTorque(Vector3.up * (turnForce * steering));
            
            Debug.DrawRay(transform.position, -transform.up * groundCheckLength , Color.red);
            
            if (throttle == 0)
            {
                driveForce = Mathf.Lerp(driveForce, 0, 100 * Time.deltaTime);
            }
        }

        driveWheelsMesh();
    }

    private void driveWheelsMesh()
    {
        foreach (var steeringWheel in driveWheels)
        {
            var wheel = steeringWheel.transform.GetChild(0);

            wheelAngle = Mathf.Clamp(wheelAngle, -maxWheelAngle, maxWheelAngle);

            wheelAngle += steering * wheelTurnTime;
            if (steering == 0)
            {
                wheelAngle = Mathf.Lerp(wheelAngle, 0, wheelTurnTime * Time.deltaTime);}
            wheel.localRotation = Quaternion.Euler(wheel.localEulerAngles.x,wheelAngle,wheel.localEulerAngles.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        driveForce = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.TransformPoint(CoM), .5f);
    }
}