using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAI : EnemySettings
{
    [Header("AI Settings")]
    [Range(1,5)]
    public float ChaseRadius = 5f;          // радиус преследования
    [Range(1,1.15f)]
    public float ChaseOffset = 1f;          // радиус прекращения преследования
    public Vector3 HomePosition;             // позиция, куда возвращается враг, если игрок вышел за пределы ChaseRadius
    public Transform SightDistance;         // поле зрения врага

    public float TimeBetweenAttack = .2f;
    public float TimeTillAttack;

    [Header("Unity Settings")]
    public Transform Target;
    public Animator Anim;

    public enum EnemyStates { Idling, Attacking, Running, ReceivingDamage, Dying };
    public EnemyStates EnemyState = EnemyStates.Idling;

    private enum LookingDirections { Left = -1, Right = 1 };     // для анимации - выбор стороны для поворота
    private KnockBack _knockBack;
    private Vector2 _currentPosition;
    private Vector2 _endPosition;
    private float distanceToTarget;
    private SpriteRenderer FlipX;

    [Header("Audio Settings")]
    private AudioSource EnemyAudioSource;
    public AudioClip[] HitSounds;

    void Awake()
    {
        _knockBack = GetComponent<KnockBack>();
        
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Anim = GetComponent<Animator>();

        IsAlive = true;
        Anim.SetBool("isAlive", IsAlive);

        SightDistance = transform.Find("VisionEndPointRight");
        SightDistance.gameObject.SetActive(true);

        var leftSightPoint = transform.Find("VisionEndPointLeft");
        leftSightPoint.gameObject.SetActive(false);

        EnemyAudioSource = GetComponentInParent<AudioSource>();

        FlipX = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (Health <= 0)
        {
            IsAlive = false;
        }

       // if (EnemyState != EnemyStates.Attacking && EnemyState != EnemyStates.ReceivingDamage && EnemyState != EnemyStates.Dying) 
       if (EnemyState == EnemyStates.Running || EnemyState == EnemyStates.Idling)
       {
            ChaseThePlayer();
            DetectPlayer();
       }
    }

    public void DetectPlayer()              // определяем, что враг находится в поле зрения игрока
    {
        _currentPosition = new Vector2(transform.position.x, SightDistance.position.y);
        _endPosition = new Vector2(SightDistance.position.x, SightDistance.position.y);

        var hits = Physics2D.LinecastAll(_currentPosition, _endPosition);

        foreach (var obj in hits)
        {
            var target = obj.collider.gameObject;

            if (target.CompareTag("Player"))   // игрок увидел противника
            {
                if (TimeTillAttack <= 0)
                {
                    StartCoroutine(AttackThePlayer(target));
                    _knockBack.HitSomeObject(target);

                    TimeTillAttack = TimeBetweenAttack;
                }
                TimeTillAttack -= Time.deltaTime;      
            }
        }
    }

    private IEnumerator AttackThePlayer(GameObject player)
    {
        EnemyState = EnemyBasicAI.EnemyStates.Attacking;
        Anim.SetBool("isAttacking", true);
        Anim.SetBool("isRunningEnemy", false);

        yield return null;

        var playerBehaviour = player.GetComponent<PlayerBehaviour>();

        StartCoroutine(playerBehaviour.ReceiveDamage(Attack));

        yield return new WaitForSeconds(.6f);

        Anim.SetBool("isAttacking", false);
        //  yield return new WaitForSeconds(.3f);    // время, которое занимает проигрыш получения удара у игрока

        yield return null;
        EnemyState = EnemyBasicAI.EnemyStates.Idling;
    }

    public IEnumerator WanishingAnimation()
    {
        if(gameObject != null)
        {
            IsAlive = false;
            EnemyState = EnemyStates.Dying;

            Anim.SetBool("isAlive", IsAlive);
            yield return null;

            yield return new WaitForSeconds(.6f);

            Destroy(gameObject);

            yield return null;
        }
    }

    public IEnumerator ReceiveDamage(int takenDamage)
    {
        Health -= takenDamage;
        EnemyState = EnemyStates.ReceivingDamage;

        Debug.Log("Enemy got hit!");
        
        Anim.SetBool("isRunningEnemy", false);
        Anim.SetBool("isReceivingDamage", true);

        EnemyAudioSource.PlayOneShot(HitSounds[Random.Range(0, HitSounds.Length)]);

        yield return null;

        yield return new WaitForSeconds(.2f);    

        if (Anim != null)
        {
            Anim.SetBool("isReceivingDamage", false);
            EnemyState = EnemyStates.Idling; 
        }

        if (gameObject != null && Health <= 0)
        {
            StartCoroutine(WanishingAnimation());
        }

        yield return null;
    }

    public void ChaseThePlayer()
    {
        var toHomePosition = new Vector3(HomePosition.x, transform.position.y, 0);   // сохраняем значение оси Oy врага, чтобы спрайт не прыгал
        var distanceToHome = Vector3.Distance(transform.position, toHomePosition);

        var toTarget = new Vector3(Target.position.x, transform.position.y, 0);
        distanceToTarget = Vector3.Distance(transform.position, Target.position);
     //   var distanceToTarget = Vector3.Distance(transform.position, toTarget);

        if (distanceToTarget <= ChaseRadius && distanceToTarget >= ChaseOffset)   // игрок в зоне преследования, а враг на HomePosition  && distanceToHome == 0 || distanceToTarget <= ChaseRadius && distanceToHome != 0
        {
            EnemyState = EnemyStates.Running;
            transform.position = Vector3.MoveTowards(transform.position, toTarget, Speed * Time.deltaTime);
            if (transform.position.x < Target.transform.position.x)       //Поворот врага в сторону гг 
            {
                FlipX.flipX = false;
            }
            if (transform.position.x > Target.transform.position.x)
            {
                FlipX.flipX = true;
            }

            AnimateRunning(toTarget);
        }
        else if (distanceToTarget > ChaseRadius && distanceToHome != 0)    // игрок вышел за пределы радиуса преследования, но враг не дошел до HomePosition
        {
            EnemyState = EnemyStates.Running;
            transform.position = Vector3.MoveTowards(transform.position, toHomePosition, Speed * Time.deltaTime);
            if (toHomePosition.x < transform.position.x)
            {
                FlipX.flipX = true;
            }
            if (toHomePosition.x > transform.position.x)
            {
                FlipX.flipX = false;
            }

            AnimateRunning(toHomePosition);
        }
        else
        {
            EnemyState = EnemyStates.Idling;
            Anim.SetBool("isRunningEnemy", false);      // idle state
        }
    }

    private void AnimateRunning(Vector3 target)
    {
        Anim.SetBool("isRunningEnemy", true);

        if (target.x > transform.position.x)
        {
            ChooseDirection(LookingDirections.Right);
        }
        else if (target.x < transform.position.x)
        {
            ChooseDirection(LookingDirections.Left);
        }

    }

    private void ChooseDirection(LookingDirections motionState)
    {
        switch (motionState)
        {
            case LookingDirections.Right:

                Anim.SetFloat("motionH", (float)LookingDirections.Right);
                SightDistance = transform.Find("VisionEndPointRight");
                SightDistance.gameObject.SetActive(true);

                var leftSightPoint = transform.Find("VisionEndPointLeft");
                leftSightPoint.gameObject.SetActive(false);

                break;

            case LookingDirections.Left:

                Anim.SetFloat("motionH", (float)LookingDirections.Left);
                SightDistance = transform.Find("VisionEndPointLeft");
                SightDistance.gameObject.SetActive(true);

                var rightSightPoint = transform.Find("VisionEndPointRight");
                rightSightPoint.gameObject.SetActive(false);

                break;
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
