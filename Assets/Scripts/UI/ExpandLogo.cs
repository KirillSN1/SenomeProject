using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandLogo : MonoBehaviour
{
    public float ExpendTime;
    public GameObject Title;
    public GameObject StartPos;
    public Transform EndPos;

    private bool Expanded=false;
    private float t=5; //Время до автомотической развертки

    void FixedUpdate()
    {
        if (!Expanded)
        {
            Title.transform.position = Vector2.Lerp(Title.transform.position, EndPos.transform.position, Time.fixedDeltaTime* ExpendTime);
            

            t = t - 0.24f * Time.fixedDeltaTime;
            if (t <= 0)
            {
                t = 5f;
                Expand();
            }
        }
        else
        {
            Title.transform.position = Vector2.Lerp(Title.transform.position, StartPos.transform.position, Time.fixedDeltaTime* ExpendTime);
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
