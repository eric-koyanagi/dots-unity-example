using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyScrollCameraZ : MonoBehaviour
{
    public float moveSpeed = 1f;

    void Update()
    {
        // Get the current position of the camera
        Vector3 currentPosition = transform.position;

        // Move the camera along the Z axis
        currentPosition.z += moveSpeed * Time.deltaTime;

        // Update the position of the camera
        transform.position = currentPosition;
    }
}
