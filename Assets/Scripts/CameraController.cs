using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // the player's transform
    public float smoothSpeed = 1f; // the smoothness of the camera's movement
    private readonly Vector3 _offset = new(0, 0, -10f); // the camera's position offset from the player

    private void LateUpdate()
    {
        var desiredPosition = target.position + _offset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
