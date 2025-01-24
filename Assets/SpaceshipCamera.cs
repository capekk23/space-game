using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target; //Target je raketa
    public Vector3 offset = new Vector3(0, 5, -10); // Offset
    public float smoothSpeed = 0.125f; // Smoothing

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target); // Make the camera look at the spaceship
    }
}

