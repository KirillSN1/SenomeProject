using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Attributes")]

    public int Health = 5;    // значение здоровья игрока не менять!
    public int Attack = 1;
    public float Speed = 4;
    public Transform SightDistance;

    [Range(1, 10)]
    public float JumpingVelocity;
    public bool IsAlive = true;

    [Header("Input Settings")]
  //  public KeyCode JumpButton = KeyCode.Space;
  //  public KeyCode AttackButton = KeyCode.E;
    public bool KeyboardInput = false;          //Управление с клавиатуры

    [HideInInspector]
    public float MInput;

    [Header("Audio settings")]
    public AudioClip[] FootstepsSounds;
    public AudioClip[] AttackSounds;
    public AudioClip[] JumpSounds;
    public AudioClip[] HitSounds;


    [Header("Unity settings")]

    [Range(1, 1.3f)]
    public float AccelerationValue = 1.066f;

    public Transform Feet;
    public float feetRadius;
    public LayerMask Groundlayer;
    public Animator Anim;

    [HideInInspector]
    public bool DoubleJump = false;

    [HideInInspector]
    public int JumpsNum;

    [HideInInspector]
    public Rigidbody2D rb;
    public bool isGrounded = false;

    private float scale;
    private Vector2 _currentPosition;
    private Vector2 _endPosition;
    private KnockBack _knockBack;     // экземпляр класса KnockBack, который отталкивает противника
    private KeyboardInput _keyboardInput;

    public AudioSource ASourse;

    public enum PlayerStates { Idling, Jumping, Falling, ReceivingDamage, Attacking, Walking, Dying };
    public PlayerStates playerState = PlayerStates.Idling;

    void Start()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
        scale = transform.localScale.x;

        _knockBack = GetComponent<KnockBack>();
        ASourse = GetComponentInChildren<AudioSource>();

        if(KeyboardInput)
        {
            _keyboardInput = GetComponent<KeyboardInput>();
        }
    }

    void Update()
    {   
        if (Health <= 0)
        {
            IsAlive = false;
        }

        if(playerState != PlayerStates.ReceivingDamage)
        {
            if (KeyboardInput)
            {
                _keyboardInput.KeyboardWalkAndAttack();
            }
            else
            {
                Walk();
            }
        }
     //   Debug.Log(playerState);

        AnimationController();
        GetPlayerStates();
    }

    public void GetPlayerStates()
    {
        if (!IsAlive)
        {
            playerState = PlayerStates.Dying;
        }
        if (Anim.GetBool("ReceiveDamage"))
        {
            playerState = PlayerStates.ReceivingDamage;
        }
        if (Anim.GetBool("Attack"))
        {
            playerState = PlayerStates.Attacking;
        }
        if (!Anim.GetBool("IsGrounded"))
        {
            if (Anim.GetFloat("JumpVeloc") > 0.01f)
            {
                playerState = PlayerStates.Jumping;
            }
            else
            {
                playerState = PlayerStates.Falling;
            }
        }
        if (Anim.GetBool("IsGrounded") && Anim.GetFloat("Speed") > 0.01f)
        {
            playerState = PlayerStates.Walking;
        }
        else if(Anim.GetBool("IsGrounded") && Anim.GetFloat("Speed") < 0.01f && !Anim.GetBool("Attack") && !Anim.GetBool("ReceiveDamage"))
        {
            playerState = PlayerStates.Idling;
        }
    }

    public IEnumerator ReceiveDamage(int takenDamage)
    {
        Anim.SetBool("ReceiveDamage", true);
        Health -= takenDamage;
        transform.GetComponent<Renderer>().material.color = Color.red;

        yield return null;

        yield return new WaitForSeconds(.3f);            // Knockback доделать

        yield return null;

        Anim.SetBool("ReceiveDamage", false);
        transform.GetComponent<Renderer>().material.color = Color.white;
    }

    public void Walk()
    {
        rb.velocity = new Vector2(MInput * Speed, rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(Feet.position, feetRadius, Groundlayer);

    }

    public void Jump()    // прыжок для мобильных устройств, вызывается по нажатию кнопки в MobileInput
    {
        if (!DoubleJump)
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * JumpingVelocity;
            }
            if (rb.velocity.y < 0) //Ускорение падения
            {
               rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * AccelerationValue);
            }
        }
        else
        {
            if (JumpsNum < 1)
            {
                ++JumpsNum;
                rb.velocity = (Vector2.up * JumpingVelocity) + new Vector2(rb.velocity.x, 0);
            }
            else if (isGrounded && JumpsNum > 0)
            {
                JumpsNum = 0;
            }
        }
    }

    public void DetectEnemy()              // определяем, что враг находится в поле зрения игрока
    {
        _currentPosition = new Vector2(transform.position.x, SightDistance.position.y);
        _endPosition = new Vector2(SightDistance.position.x, SightDistance.position.y);

        var hits = Physics2D.LinecastAll(_currentPosition, _endPosition);

        foreach (var obj in hits)
        {
            var target = obj.collider.gameObject;

            if (target.CompareTag("Enemy"))   // игрок увидел противника
            {
                StartCoroutine(AttackTheEnemy(target));           // атаковать противника
                _knockBack.HitSomeObject(target);
            }
            else
            {
                StartCoroutine(AttackTheEnemy(null));    // если игрок не видит врага - просто влючить анимацию взамаха меча
            }
        }
    }

    private IEnumerator AttackTheEnemy(GameObject enemy)
    {
        Anim.SetBool("Attack", true);
        if (Anim.GetBool("Attack"))

        yield return null;

        if (enemy!= null)     // если врага нет - в методе просто проигрывается анимация взмаха меча
        {
           var enemyBasicAI = enemy.GetComponent<EnemyBasicAI>();
           StartCoroutine(enemyBasicAI.ReceiveDamage(Attack));
        }

        yield return new WaitForSeconds(.1f);

        Anim.SetBool("Attack", false);
    }

    public void AnimationController()
    {
        Anim.SetFloat("Speed", Mathf.Abs(MInput));
        Anim.SetFloat("JumpVeloc", rb.velocity.y);

        if (MInput < 0)
        {
            transform.localScale = new Vector3(-scale, transform.localScale.y, transform.localScale.z);
        }
        else if (MInput > 0)
        {
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        }

        Anim.SetBool("IsGrounded", isGrounded);
    }

    public void PlayAudioClipEvent()
    {
        if (Anim.GetBool("Attack"))
        {
            ASourse.PlayOneShot(AttackSounds[Random.Range(0, AttackSounds.Length)]);
        }
        if (Anim.GetFloat("Speed") > 0.01f && isGrounded == true)
        {
            ASourse.PlayOneShot(FootstepsSounds[Random.Range(0, FootstepsSounds.Length)]);
        }
        if (Anim.GetFloat("JumpVeloc") > 0.01f)
        {
            ASourse.PlayOneShot(JumpSounds[Random.Range(0, JumpSounds.Length)]);
        }
        if (Anim.GetBool("ReceiveDamage"))
        {
            ASourse.PlayOneShot(HitSounds[Random.Range(0, HitSounds.Length)]);
        }
    }

    void OnDrawGizmosSelected()      // показывает поле зрения игрока
    {    
        Gizmos.color = Color.red;

        _currentPosition = new Vector2(transform.position.x, SightDistance.position.y);
        _endPosition = new Vector2(SightDistance.position.x, SightDistance.position.y);

        Gizmos.DrawLine(_currentPosition, _endPosition);
    }
}
