using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpikes : MonoBehaviour
{
    public float TimeBetweenAttack = .02f;
    public float TimeTillAttack;
    public int DamageAmount = 1;
    public KnockBack _knockBack;
    public bool IsOnSpike = false;

    void Start()
    {
        TimeTillAttack = 0;
    }

    void Update()
    {
        if (IsOnSpike)
        {
            TimeTillAttack -= Time.deltaTime;

            if (TimeTillAttack < 0)
            {
                TimeTillAttack = 0;
            }
        }

        if (TimeTillAttack <= 0 && IsOnSpike)
        {
            HurtTarget();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            TimeTillAttack = 0;
        }   
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsOnSpike = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        IsOnSpike = false;
        TimeTillAttack = TimeBetweenAttack;
    }

    private void HurtTarget()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        StartCoroutine(player.ReceiveDamage(DamageAmount));
        
        
        TimeTillAttack = TimeBetweenAttack;      
    }
}
