using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [Header("Keyboard Input Settings")]
    public KeyCode JumpButton = KeyCode.Space;
    public KeyCode AttackButton = KeyCode.E;

    private PlayerBehaviour _player;    

    public AnimationCurve JumpCurve;
    public float JumpTime;

    void Start()
    {
        _player = GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        if (_player.Acc)
        {
            _player.Speed = Mathf.Lerp(_player.Speed, _player.AccelerationPower, _player.AccelerationTime * Time.deltaTime);
        }
    }

    public void KeyboardWalkAndAttack()
    {
        _player.MInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(AttackButton))      // атаковать enemy
        {
            Debug.Log("Pressing E");
            _player.DetectEnemy();
        }
        if (Input.GetKey(AttackButton))
        {
            _player.Anim.SetTrigger("AttackButtonPresed");
        }
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            _player.Acc = true;
        }
        else
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            _player.Acc = false;
            _player.Speed = 0f;
        }

        _player.rb.velocity = new Vector2(_player.MInput * _player.Speed, _player.rb.velocity.y);
        _player.isGrounded = Physics2D.OverlapCircle(_player.Feet.position, _player.feetRadius, _player.Groundlayer);

        KeyboardJump();
    }

    public void KeyboardJump()
    {
        if (!_player.DoubleJump)
        {
            if (Input.GetKeyDown(JumpButton) && _player.isGrounded)
            {
                _player.JumpingVelocity = JumpCurve.Evaluate(JumpTime);
                _player.rb.velocity = Vector2.up * _player.JumpingVelocity;
            }
            if (_player.rb.velocity.y < 0)            //Ускорение падения
            {
                _player.rb.velocity = new Vector2(_player.rb.velocity.x, _player.rb.velocity.y * _player.FallAccelerationValue);
            }
        }
        else
        {
            if (Input.GetKeyDown(JumpButton) && _player.JumpsNum < 1)
            {
                ++_player.JumpsNum;
                _player.rb.velocity = (Vector2.up * _player.JumpingVelocity) + new Vector2(_player.rb.velocity.x, 0);
            }
            else if (_player.isGrounded && _player.JumpsNum > 0)
            {
                _player.JumpsNum = 0;
            }
        }
    }

}
