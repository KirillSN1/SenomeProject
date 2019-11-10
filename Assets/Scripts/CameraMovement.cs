using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    public Vector2 max;
    public Vector2 min;

    void Start()
    {

    }


    void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            var targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, min.x, max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, min.y, max.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);  // альтернативно, чтобы не было скачков, для движущихся обьектов можно использовать Vector3.SmoothDamp
        }
    }
}
