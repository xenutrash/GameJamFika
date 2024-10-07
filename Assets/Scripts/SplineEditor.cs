using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;
[ExecuteInEditMode()]
public class SplineEditor : MonoBehaviour
{

    [SerializeField] private SplineContainer splineContainer;

    [SerializeField] private int splineIndex;
    [SerializeField] [Range(0f,1f)] private float time;

    float3 position;
    float3 tangent;
    float3  upVector;

    // Update is called once per frame
    void Update()
    {
        splineContainer.Evaluate(splineIndex, time, out position, out tangent, out upVector);
    }

    private void OnDrawGizmos()
    {
        Handles.matrix = transform.localToWorldMatrix;
        Handles.SphereHandleCap(0, position, Quaternion.identity, 1f, EventType.Repaint);
    }
}
