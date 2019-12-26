using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBoxBasic : MonoBehaviour
{
    BasicBehavior bb;
   
    void Awake()
    {
        bb = transform.parent.gameObject.GetComponent(typeof (BasicBehavior)) as BasicBehavior;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            bb.Attack(other.gameObject);
        }
    }
    
}
