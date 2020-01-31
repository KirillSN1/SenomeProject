using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    PlayerBehaviour player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }
   /// <summary>
   /// Sent when an incoming collider makes contact with this object's
   /// collider (2D physics only).
   /// </summary>
   /// <param name="other">The Collision2D data associated with this collision.</param>
   void OnCollisionEnter2D(Collision2D other)
   {
       player.currentPlatform = GetComponent<BoxCollider2D>();
   }
}
