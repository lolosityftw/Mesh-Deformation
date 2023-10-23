using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smImpact : MonoBehaviour
{
    private float impactForce;
    public float impactStrength = 10;
    private Rigidbody rb;

    private float vel1;
    private float vel2;

    private GameObject otherGO;

    public float maxRange = 1;
    public float distanceDropOff = 10;
    public float maxForce;

    private Vector3 impactDir;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        otherGO = null;
        otherGO = other.gameObject;

        if (otherGO.GetComponent<Rigidbody>())
        {
            var rbOther = other.gameObject.GetComponent<Rigidbody>();

            vel1 = 1 + rb.velocity.magnitude;
            vel2 = 1 + rbOther.velocity.magnitude;

            impactForce = (vel1 * vel2) * rbOther.mass;
            impactDir = transform.position - otherGO.transform.position;

            getClosestNodes();
        }
    }

    private void getClosestNodes()
    {
        var nodes = GetComponent<smController>().nodes;

        for (int i = 0; i < nodes.Length; i++)
        {
            var distance = Vector3.Distance(nodes[i].transform.position, otherGO.transform.position);
            var _impactForce = (impactForce / 1000) * impactStrength;
            var _dropOffForce = 1 + (distance * distance * distanceDropOff);
            var appliedForce = _impactForce / _dropOffForce;
            
            if (distance < maxRange )
            {
                if (appliedForce < maxForce)
                {
                    nodes[i].transform.position = nodes[i].transform.position + impactDir * appliedForce / 1000;
                }
                else
                {
                    nodes[i].transform.position = nodes[i].transform.position + impactDir * maxForce ;
                }
               
                 Debug.Log("Node: "+i+" Distance: "+distance + " impact: "+_impactForce + " dropOff: "+_dropOffForce+ " Total: "+appliedForce);
            }
        }
    }
}