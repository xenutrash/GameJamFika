using UnityEngine;

public class RotateVehicle : MonoBehaviour
{
    public float raycastLength = 2f;    
    public LayerMask groundLayer;      
    public float rotationSpeed = 5f;  

    public Vector3 frontOffset = new Vector3(0, 0, 1f);
    public Vector3 backOffset = new Vector3(0, 0, -1f);
    public Vector3 leftOffset = new Vector3(-1f, 0, 0);
    public Vector3 rightOffset = new Vector3(1f, 0, 0);

    private RaycastHit hitInfoFront, hitInfoBack, hitInfoLeft, hitInfoRight;

    void Update()
    {
        bool frontHit = Physics.Raycast(transform.TransformPoint(frontOffset), -transform.up, out hitInfoFront, raycastLength, groundLayer);
        bool backHit = Physics.Raycast(transform.TransformPoint(backOffset), -transform.up, out hitInfoBack, raycastLength, groundLayer);
        bool leftHit = Physics.Raycast(transform.TransformPoint(leftOffset), -transform.up, out hitInfoLeft, raycastLength, groundLayer);
        bool rightHit = Physics.Raycast(transform.TransformPoint(rightOffset), -transform.up, out hitInfoRight, raycastLength, groundLayer);

        if (frontHit || backHit || leftHit || rightHit)
        {
            AlignVehicleWithGround(frontHit, backHit, leftHit, rightHit);
        }
    }

    void AlignVehicleWithGround(bool frontHit, bool backHit, bool leftHit, bool rightHit)
    {
        Vector3 combinedNormal = Vector3.zero;
        int hitCount = 0;

        if (frontHit)
        {
            combinedNormal += hitInfoFront.normal;
            hitCount++;
        }
        if (backHit)
        {
            combinedNormal += hitInfoBack.normal;
            hitCount++;
        }
        if (leftHit)
        {
            combinedNormal += hitInfoLeft.normal;
            hitCount++;
        }
        if (rightHit)
        {
            combinedNormal += hitInfoRight.normal;
            hitCount++;
        }

        if (hitCount > 0)
        {
            combinedNormal /= hitCount;

            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, combinedNormal) * transform.rotation;

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        DrawRaycastGizmo(frontOffset, raycastLength);
        DrawRaycastGizmo(backOffset, raycastLength);
        DrawRaycastGizmo(leftOffset, raycastLength);
        DrawRaycastGizmo(rightOffset, raycastLength);
    }

    private void DrawRaycastGizmo(Vector3 offset, float length)
    {
        Vector3 start = transform.TransformPoint(offset);
        Vector3 end = start - transform.up * length;
        Gizmos.DrawLine(start, end);
    }
}
