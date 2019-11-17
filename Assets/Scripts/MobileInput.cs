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
        if (!main.DoubleJump)
        {

            if (main.isGrounded)
            {
                main.rb.velocity = Vector2.up * main.JumpingVelocity;
            }
            if (main.rb.velocity.y < 0) //Ускорение падения
            {
                main.rb.velocity = new Vector2(main.rb.velocity.x, main.rb.velocity.y * main.AccelerationValue);
            }
        }
        else
        {
            if (main.JumpsNum < 1)
            {
                ++main.JumpsNum;
                main.rb.velocity = (Vector2.up * main.JumpingVelocity) + new Vector2(main.rb.velocity.x, 0);
            }
            else
            if (main.isGrounded && main.JumpsNum > 0)
            {
                main.JumpsNum = 0;
            }
        }
    }

    public void Attack()
    {
        main.DetectEnemy();
    }

}
