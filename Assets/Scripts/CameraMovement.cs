using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;
    [SerializeField] private Vector2 maxPos, minPos;

    private Vector3 targetPosition;

    // Update is called once per frame
    void LateUpdate()
    {
        //calculate  position using target xy and camera z
        targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        //clamp into boundaries
        targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);

        //bring camera to target
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
