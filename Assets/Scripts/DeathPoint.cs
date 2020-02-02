using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPoint : MonoBehaviour
{
  //  private PlayerBehaviour player;

    void Start()
    {
      //  player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

            //GetComponent<>
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            KillThePlayer(other);
        }
    }

    private void KillThePlayer(Collider2D player)
    {
        var playerState = player.GetComponent<PlayerBehaviour>();
        playerState.Health = 0;
        playerState.IsAlive = false;

        //Destroy(player, 0.5f);
    }
}
