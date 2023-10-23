using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_wheel : MonoBehaviour
{
    public Rigidbody rb;
    
    public float springStiffness;
    public float springLength;
    public float springOrigin;

    public float damping;
    public float friction;

    public float springForce;

    private float lastContactDepth = 0;

    public LayerMask ground;

    public GameObject wheelMesh;
    public float wheelRadius;

    public float dot;

    // public float drag;
    


    private void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();
        wheelMesh = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up * springOrigin, -transform.up, out hit, springLength, ground))
        {
            //Spring Force
            var contactDepth = hit.distance - springLength;
            springForce = -springStiffness * contactDepth;

            //Damp Force
            var compressionSpeed = contactDepth - lastContactDepth;
            var dampForce = -damping * compressionSpeed;

            var springVector = springForce + dampForce;

            //Spring
            rb.AddForceAtPosition(springVector * transform.up, hit.point);

            //Friction
            var damp = -rb.velocity * friction;
            rb.AddForceAtPosition(damp, hit.point);
            

            lastContactDepth = hit.distance - springLength;

            wheelMesh.transform.position = hit.point + transform.up * wheelRadius;

            dot = Vector3.Dot(transform.right.normalized, rb.velocity.normalized);
            var vdrag = Vector3.Dot(transform.forward.normalized, rb.velocity.normalized);
            
            rb.AddForce(transform.right * -dot * rb.mass);
            // rb.AddForce(transform.forward * -vdrag * drag);

            Debug.DrawRay(transform.position + transform.up * springOrigin, -transform.up * springLength, Color.yellow);
            Debug.DrawRay(hit.point, hit.normal, Color.cyan);
            
        } 
        
   

   

      
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + transform.up * springOrigin, .1f);
     
       
    }
}