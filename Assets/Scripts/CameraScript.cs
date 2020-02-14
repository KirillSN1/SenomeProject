using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float MaxX;
    [SerializeField] float MinX;
    [SerializeField] float MaxY;
    [SerializeField] float MinY;
    float speed = 12f;
    public Transform Player;

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(Player.position.x,MinX,MaxX) ,Mathf.Clamp(Player.position.y,MinY,MaxY) ,transform.position.z);
    }
}

