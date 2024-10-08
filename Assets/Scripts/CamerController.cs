using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamerController : MonoBehaviour
{


    public enum cameraState { right, left, movingRight, movingLeft };
    public cameraState state = cameraState.right;

    public float moveSpeed = 20;

    private Vector3 positionRight, positionLeft;

    void Awake()
    {
        positionRight = new Vector3(2, 2, 1);
        positionLeft = new Vector3(-2, 2, 1);
    }

    public void Update()
    {
        if (state == cameraState.movingRight)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, positionRight, moveSpeed * Time.deltaTime);

            if (transform.localPosition == positionRight)
                state = cameraState.right;
        }
        else if (state == cameraState.movingLeft)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, positionLeft, moveSpeed * Time.deltaTime);

            if (transform.localPosition == positionLeft)
                state = cameraState.left;
        }


        if (Input.GetKeyDown("x"))
        {
            if ((state == cameraState.right) || (state == cameraState.movingRight))  // Switch it to Left
            {
                state = cameraState.movingLeft;
            }
            else if ((state == cameraState.left) || (state == cameraState.movingLeft))   // Switch it to Right
            {
                state = cameraState.movingRight;
            }
        }
    }
}


