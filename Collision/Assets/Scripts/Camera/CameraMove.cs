using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    private bool Is2D;
    private float proportion;
    private static float epsilon = 3f;
    private static float cameraZ = -1f;
    private Vector3 moveVector;
    private Vector3 startMousePosition;
    private Vector3 startCameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        Is2D = true;
        proportion =100f/ Camera.main.orthographicSize;
       
    }

    private bool IsSmall(Vector3 vector, float error)
    {
        if (vector.magnitude < error)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // onPress = true;
            startMousePosition = Input.mousePosition;
            startCameraPosition = this.transform.position;

            if (Is2D)
            {
                startMousePosition.z = default;
                startCameraPosition.z = cameraZ;
            }
        }

        if (Input.GetMouseButton(0))
        {
            moveVector = Input.mousePosition- startMousePosition;
            moveVector /= proportion;
            if (Is2D)
            {
                moveVector.z = default;
            }

            if (!IsSmall(moveVector, epsilon))
            {

                this.transform.position = startCameraPosition - moveVector;
                
            }
        }


    }
}
