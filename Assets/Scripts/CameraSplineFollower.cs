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

    public float distance = 5.0f;  // Distance behind the kart
    public float height = 2.0f;    // Height above the kart
    public float lookAheadDistance = 5.0f; // How far ahead of the kart the camera looks
    public float smoothSpeed = 0.125f;

    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0, height, -distance);
    }

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

        /*transform.rotation = Quaternion.LookRotation(tangent, Vector3.up);*/

        Vector3 desiredPosition = transform.position + offset;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Calculate a point ahead of the kart that the camera should look at
        Vector3 lookAtPoint = transform.position + transform.forward * lookAheadDistance;

        // Make the camera look at the point ahead of the kart
        transform.LookAt(lookAtPoint);
    }
}
