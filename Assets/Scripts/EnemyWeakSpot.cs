using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakSpot : MonoBehaviour
{
    public bool _playerOnHead;
    private GameObject Player;
    private Vector2 Direction;

    public float x = 200;
    public float y = 10;
    private float a;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {

        Direction = new Vector2(a, y);
        if (_playerOnHead)
        {
            if (Player.transform.position.x > gameObject.transform.position.x)
            {
                a = x;
            }
            else
            {
                a = -x;
            }
            Player.GetComponent<Rigidbody2D>().AddForce(Direction);
        }
    }

    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            _playerOnHead = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(ForceOff());
    }

    IEnumerator ForceOff()
    {
        yield return new WaitForSeconds(0.5f);
        _playerOnHead = false;
    }


}
