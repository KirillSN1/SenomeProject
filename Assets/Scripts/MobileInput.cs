using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour
{
    private PlayerBehaviour main;

    private float MInput;
    private bool left;
    private bool right;
    

    private void Start()
    {
        main = GetComponent<PlayerBehaviour>();
    }

    private void Update()
    {
        if (left)
        {
            MInput = -1;
        }
        if (left==false && right==false)
        {
            MInput = 0;
        }
        if (right)
        {
            MInput = 1;
        }
        main.MInput = MInput;
        if (main.Acc)
        {
            main.Speed = Mathf.Lerp(main.Speed, main.AccelerationPower, main.AccelerationTime * Time.deltaTime);
        }
        else
        {
            main.Speed = Mathf.Lerp(main.Speed, 0f, main.DecelerationTime * Time.deltaTime);
        }
    }

    public void LeftDown()
    {
        left = true;
        main.Acc = true;
        main.runDir = main.MInput;
    }
    public void LeftUp()
    {
        left = false;
        main.Acc = false;
        main.runDir = main.MInput;
    }

    public void RightDown()
    {
        right = true;
        main.Acc = true;
        main.runDir = main.MInput;
    }

    public void RightUp()
    {
        right = false;
        main.Acc = false;
        main.runDir = main.MInput;
    }

    public void Jump()
    {
        main.Jump();
    }

    public void Attack()
    {
        if (main.Anim.GetBool("Attack") == false)
        {
            main.DetectEnemy();
        }
    }

}
