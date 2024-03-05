using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    Vector3 _offset;
    [SerializeField] Transform cameraTarget;
    [SerializeField] float smoothTime;
    Vector3 _currentVelocity = Vector3.zero;

    private void Awake() {
        _offset = transform.position - cameraTarget.position;
    }

    private void LateUpdate() {
        Vector3 targetPosition = cameraTarget.position + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }

}
