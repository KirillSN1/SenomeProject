using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    private PlayerBehaviour playerBehaviour;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Rabbit Player")
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
            player.AddingLife();
            Destroy(gameObject);
        }
    }
}
