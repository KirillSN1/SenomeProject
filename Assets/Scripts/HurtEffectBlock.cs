using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEffectBlock : MonoBehaviour
{
    public int DamageAmount = 1;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.CompareTag("Player"))
        {
           var playerBehaviour = obj.GetComponent<PlayerBehaviour>();
           StartCoroutine(playerBehaviour.ReceiveDamage(DamageAmount));
        }

        if (obj.gameObject.CompareTag("Enemy"))
        {
           var enemyBasicAI = obj.GetComponent<EnemyBasicAI>();
           StartCoroutine(enemyBasicAI.ReceiveDamage(DamageAmount));
        }
    
    }
}
