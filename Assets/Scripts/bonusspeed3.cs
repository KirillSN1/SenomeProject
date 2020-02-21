using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusspeed3 : MonoBehaviour
{
    public PlayerBehaviour _player;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _player.Speed -= 5f;
        }
    }

}