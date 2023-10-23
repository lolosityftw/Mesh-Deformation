using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smNode : MonoBehaviour
{
    public GameObject parent;
    public GameObject targetObj;

    public Vector3[] orgVerts, newVerts;
    public List<int> selectedVertsPos;

    public float nodeSize = 0.25f;

    private Vector3 movOffset;
    private Vector3 targetObjpos;
    

    private void Start()
    {
        for (int i = 0; i < newVerts.Length; i++)
        {
            var distance = Vector3.Distance(targetObj.transform.InverseTransformPoint(transform.position), newVerts[i]);
            if (distance < nodeSize && !selectedVertsPos.Contains(i))
            {
                selectedVertsPos.Add(i);
            }
            else
            {
                selectedVertsPos.Remove(i);
            }
        }
    }

    private void FixedUpdate()
    {
        targetObjpos = targetObj.transform.InverseTransformPoint(transform.position);

        for (int i = 0; i < selectedVertsPos.Count; i++)
        {
            newVerts[selectedVertsPos[i]] = newVerts[selectedVertsPos[i]] + movOffset;
        }

        if (movOffset != Vector3.zero)
        {
            newVerts = parent.GetComponent<smController>().newVerts;
        }
    }

    private void LateUpdate()
    {
        movOffset = targetObj.transform.InverseTransformPoint(transform.position) - targetObjpos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, nodeSize);
    }
}