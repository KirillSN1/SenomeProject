using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Detect : MonoBehaviour
{
   BasicBehavior bb;
   
    void Awake()
    {
        bb = transform.parent.gameObject.GetComponent(typeof (BasicBehavior)) as BasicBehavior;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            bb.chooseDirection(other.gameObject);
        }
    }
    
   
}
