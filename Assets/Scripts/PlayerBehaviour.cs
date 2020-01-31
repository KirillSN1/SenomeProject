using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Attributes")]

    public int Health = 5;    // значение здоровья игрока не менять!
    public int Attack = 1;
    public float Speed = 4;
    public Transform SightDistance;    // поле зрения игрока
    public Transform Platform;
    [Range(1, 10)]
    public float JumpingVelocity;

    public bool IsAlive = true;
    public bool isGrounded = false;

    [Header("Enemy Settings")]
    [Range(0, 1)]
    public float DamageTime;

    [Header("Input Settings")]
    //   public KeyCode JumpButton = KeyCode.Space;
    //  public KeyCode AttackButton = KeyCode.E;
    public bool KeyboardInput = false;          //Управление с клавиатуры

    //[HideInInspector]
    public float MInput;

    [Header("Audio settings")]
    public AudioClip[] FootstepsSounds;
    public AudioClip[] AttackSounds;
    public AudioClip[] JumpSounds;
    public AudioClip[] HitSounds;

    [Header("Unity settings")]

    [Range(1, 1.3f)]
    public float FallAccelerationValue = 1.055f;
    [HideInInspector]
    public bool Acc;
    [HideInInspector]
    public float AccelerationPower = 4f;
    [Range(1, 6)]
    public float AccelerationTime = 6f;
    [Range(1, 6)]
    public float DecelerationTime = 6f;
    [HideInInspector]
    public float runDir;
    [Space(25f)]

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

    private float scale;
    private Vector2 _currentPosition;
    private Vector2 _endPosition;
    
    
    private KnockBack _knockBack;     // экземпляр класса KnockBack, который отталкивает противника
    private KeyboardInput _keyboardInput;
    public List<GameObject> GameObjectsinView = new List<GameObject>();
    public Collider2D currentPlatform;
    public AudioSource ASourсe;

    public enum PlayerStates { Idling, Jumping, Falling, ReceivingDamage, Attacking, Walking, Dying };
    public PlayerStates State = PlayerStates.Idling;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
        scale = transform.localScale.x;

        _knockBack = GetComponent<KnockBack>();

        ASourсe = GetComponentInChildren<AudioSource>();

        if (KeyboardInput)
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

        GetPlayerStates();               // при мерже - оставить эту строку

        if (State != PlayerStates.ReceivingDamage)
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

        AnimationController();
    }

    public void GetPlayerStates()
    {
        if (!IsAlive)
        {
            State = PlayerStates.Dying;
        }
        if (Anim.GetBool("ReceiveDamage"))
        {
            State = PlayerStates.ReceivingDamage;
        }
        if (Anim.GetBool("Attack"))
        {
            State = PlayerStates.Attacking;
        }
        if (!Anim.GetBool("IsGrounded"))
        {
            if (Anim.GetFloat("JumpVeloc") > 0.01f)
            {
                State = PlayerStates.Jumping;
            }
            else
            {
                State = PlayerStates.Falling;
            }
        }
        if (Anim.GetBool("IsGrounded") && Anim.GetFloat("Speed") > 0.01f)
        {
            State = PlayerStates.Walking;
        }
        else if (Anim.GetBool("IsGrounded") && Anim.GetFloat("Speed") < 0.01f && !Anim.GetBool("Attack") && !Anim.GetBool("ReceiveDamage"))
        {
            State = PlayerStates.Idling;
        }
    }

    
    public IEnumerator ReceiveDamage(int takenDamage)
    {
        Anim.SetBool("ReceiveDamage", true);
        Health -= takenDamage;
        transform.GetComponent<Renderer>().material.color = Color.red;
        //Handheld.Vibrate();                              //Вибрация

        yield return null;

        yield return new WaitForSeconds(.3f);            // Knockback доделать

        yield return null;

        Anim.SetBool("ReceiveDamage", false);
        transform.GetComponent<Renderer>().material.color = Color.white;
    }

    public void Walk()
    {
        if (Acc)
        {
            rb.velocity = new Vector2(MInput * Speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(runDir * Speed, rb.velocity.y);
        }
        
        isGrounded = Physics2D.OverlapCircle(Feet.position, feetRadius, Groundlayer);
    }

    public void Jump()    // прыжок для мобильных устройств, вызывается по нажатию кнопки в MobileInput
    {
        if (!DoubleJump)
        {
            if (isGrounded && State != PlayerStates.ReceivingDamage)
            {
                rb.velocity = Vector2.up * JumpingVelocity * 3;
            }
            if (rb.velocity.y < 0) //Ускорение падения
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * FallAccelerationValue);
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

          if(State!= PlayerStates.Attacking && State!= PlayerStates.Walking && State!=PlayerStates.ReceivingDamage)
            {
                if (target.CompareTag("Enemy"))   // игрок увидел противника
                {
                        StartCoroutine(AttackTheEnemy(target));           // атаковать противника

                    _knockBack.HitSomeObject(target);
                }
                else if (target.CompareTag("Chest"))
                {
                    StartCoroutine(OpenChest(target));
                }
                else
                {
                    StartCoroutine(AttackTheEnemy(null));    // если игрок не видит врага - просто влючить анимацию взамаха меча
                }
            }
        }
    }

    private IEnumerator AttackTheEnemy(GameObject enemy)
    {
        if (Anim.GetBool("Attack") == false)
        Anim.SetBool("Attack", true);

        yield return null;
        yield return new WaitForSeconds(GetComponent<Animation>().clip.length*DamageTime);
        
        if (enemy != null )     // если врага нет - в методе просто проигрывается анимация взмаха меча
        {
            var enemyBasicAI = enemy.GetComponent<EnemyBasicAI>();
            var enemyBasicBeh = enemy.GetComponent<BasicBehavior>();
            if (enemyBasicAI != null)
            StartCoroutine(enemyBasicAI.ReceiveDamage(Attack));
            else
            StartCoroutine(enemyBasicBeh.ReceiveDamage(Attack));
        }
        

        yield return new WaitForSeconds(GetComponent<Animation>().clip.length);

        Anim.SetBool("Attack", false);
    }

    private IEnumerator OpenChest(GameObject target)
    {
        ParametersOfGeneration pof = target.GetComponent<ParametersOfGeneration>();
        GameObject artefactPrefub = target.transform.Find("Artefact").gameObject;
        pof.ArtefactCreated(artefactPrefub, target);
        target.transform.GetComponent<Renderer>().material.color = Color.gray;
        yield return null;
    }
    public void AnimationController()
    {
        Anim.SetFloat("Speed", Mathf.Abs(MInput));
        Anim.SetFloat("JumpVeloc", rb.velocity.y);
        if (State == PlayerStates.Walking) 
        {
            Anim.speed = rb.velocity.x / 5; //Изменение скорости анимации бега в зависимости от скорости персонажа.
        }

        if (MInput < 0)
        {
            transform.localScale = new Vector3(-scale, transform.localScale.y, transform.localScale.z);
        }
        else if (MInput > 0)
        {
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
        }

        Anim.SetBool("IsGrounded", isGrounded);

        if(GameManager.Gm.GameState == GameManager.GameStates.BeatLevel)
        {
            Anim.SetBool("LevelCompleted", true);
        }
    }

    public void PlayAudioClipEvent()
    {
        if (Anim.GetBool("Attack"))
        {
            ASourсe.PlayOneShot(AttackSounds[Random.Range(0, AttackSounds.Length)]);
        }
        if (Anim.GetFloat("Speed") > 0.01f && isGrounded == true)
        {
            ASourсe.PlayOneShot(FootstepsSounds[Random.Range(0, FootstepsSounds.Length)]);
        }
        if (Anim.GetFloat("JumpVeloc") > 0.01f)
        {
            ASourсe.PlayOneShot(JumpSounds[Random.Range(0, JumpSounds.Length)]);
        }
        if (Anim.GetBool("ReceiveDamage"))
        {
            ASourсe.PlayOneShot(HitSounds[Random.Range(0, HitSounds.Length)]);
        }
    }

    //void OnDrawGizmosSelected()      // показывает поле зрения игрока
    //{    
    //    Gizmos.color = Color.red;

    //    _currentPosition = new Vector2(transform.position.x, SightDistance.position.y);
    //    _endPosition = new Vector2(SightDistance.position.x, SightDistance.position.y);

    //    Gizmos.DrawLine(_currentPosition, _endPosition);
    //}

    void OnDrawGizmosSelected()      // показывает поле зрения игрока
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(Feet.position, feetRadius);
    }
}
