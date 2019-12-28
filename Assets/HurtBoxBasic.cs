using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBoxBasic : MonoBehaviour
{
    BasicBehavior bb;
    public float TimeBetweenAttack = .5f;  
    public float TimeTillAttack    = 0.0f;

    void Awake()
    {
        bb = transform.parent.gameObject.GetComponent(typeof (BasicBehavior)) as BasicBehavior;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            if (TimeTillAttack <= 0)
            {
                TimeTillAttack = TimeBetweenAttack;
                StartCoroutine(bb.Attack(other.gameObject));
            }
            TimeTillAttack -= Time.deltaTime;
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        TimeTillAttack = TimeBetweenAttack;
        bb.Idle();
    }
    
}
