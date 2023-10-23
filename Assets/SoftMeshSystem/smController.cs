using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smController : MonoBehaviour
{
    public GameObject targetObj;
    // [HideInInspector]
    public Vector3[] orgVerts, newVerts;
    
    public GameObject[] nodes;
    public float nodeSize;
    
    private void Awake()
    {
        orgVerts = targetObj.GetComponent<MeshFilter>().mesh.vertices;
        newVerts = new Vector3[orgVerts.Length];
        
        for (int i = 0; i < orgVerts.Length; i++)
        {
            newVerts[i] = orgVerts[i];
        }
        
        for (int i = 0; i < nodes.Length; i++)
        {
            var node = nodes[i].GetComponent<smNode>();
            node.orgVerts = orgVerts;
            node.newVerts = newVerts;
            node.parent = transform.gameObject;
            node.targetObj = targetObj;
            node.nodeSize = nodeSize;
        }
    }
    
    private void LateUpdate()
    {
        targetObj.GetComponent<MeshFilter>().mesh.vertices = newVerts;
        targetObj.GetComponent<MeshFilter>().mesh.RecalculateNormals();
    }
}
