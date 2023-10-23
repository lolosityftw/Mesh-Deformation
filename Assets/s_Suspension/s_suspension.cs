using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_suspension : MonoBehaviour
{
   
    public float springLength;
    public float springOrigin;
    

    public GameObject[] Wheels;

    private void Start()
    {
        foreach (var wheel in Wheels)
        {
            var wh = wheel.GetComponent<s_wheel>();
            wh.springOrigin = springOrigin;
            wh.springLength = springLength;
           
        }
    }

    private void FixedUpdate()
    {
        foreach (var wheel in Wheels)
        {
            var wh = wheel.GetComponent<s_wheel>();
            wh.springOrigin = springOrigin;
            wh.springLength = springLength;
            
        }
    }
}
