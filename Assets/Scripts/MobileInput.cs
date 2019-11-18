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
    }

    public void LeftDown()
    {
        left = true;
    }
    public void LeftUp()
    {
        left = false;
    }

    public void RightDown()
    {
       right = true;
    }

    public void RightUp()
    {
        right = false;
    }

    public void Jump()
    {
        main.Jump();
    }

    public void Attack()
    {
        main.DetectEnemy();
    }

}
