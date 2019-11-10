using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEffectBlock : MonoBehaviour
{

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.CompareTag("Player"))
        {
           var playerHealth = obj.GetComponent<PlayerBehaviour>().Health;

           playerHealth -= 1;

           obj.GetComponent<PlayerBehaviour>().Health = playerHealth;
        }

        if (obj.gameObject.CompareTag("Enemy"))
        {
           var enemyHealth = obj.GetComponent<EnemySettings>().Health;

           enemyHealth -= 1;

           obj.GetComponent<EnemySettings>().Health = enemyHealth;
        }
    
    }
}
