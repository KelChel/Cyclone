using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.4f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = target.position + offset;
        transform.position = targetPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            targetPosition = target.position + offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition,ref velocity, smoothTime);
        }
    }
}
