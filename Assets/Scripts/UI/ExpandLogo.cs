using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandLogo : MonoBehaviour
{
    public float time;
    public GameObject Title;
    public GameObject StartPos;
    public Transform EndPos;

    private bool Expanded=false;

    void FixedUpdate()
    {
        if (!Expanded)
        {
            Title.transform.position = Vector2.Lerp(Title.transform.position, EndPos.transform.position, Time.fixedDeltaTime*time);
        }
        else
        {
            Title.transform.position = Vector2.Lerp(Title.transform.position, StartPos.transform.position, Time.fixedDeltaTime*time);
        }
    }

    public void Expand()
    {
        if (!Expanded)
        {
            Expanded = true;
        }
        else Expanded = false;
    }
}
