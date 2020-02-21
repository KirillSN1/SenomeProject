using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusspeed2 : MonoBehaviour
{
    public PlayerBehaviour _player;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _player.Speed -= 10f;
        }
    }

}