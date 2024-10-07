using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;

public class CameraSplineFollower : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private int splineIndex = 0; 
    [SerializeField][Range(0f, 1f)] private float t;  
    [SerializeField] private float speed = 0.1f;  
    private float3 position, tangent, upVector;

    // Update is called once per frame
    void Update()
    {
        if (splineContainer == null || splineIndex < 0 || splineIndex >= splineContainer.Splines.Count)
        {
            Debug.LogError("SplineContainer or SplineIndex is not properly set.");
            return;
        }

        t += speed * Time.deltaTime;
        if (t > 1f)
        {
            t = 0f;  
        }

        splineContainer.Evaluate(splineIndex, t, out position, out tangent, out upVector);

        transform.position = position;

        transform.rotation = Quaternion.LookRotation(tangent, Vector3.up);
    }
}
