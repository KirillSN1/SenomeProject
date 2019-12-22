using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakSpot : MonoBehaviour
{
    public bool _playerOnHead;
    private GameObject Player;
    private Vector2 Direction;
    private EnemyBasicAI enemyBasicAI;
    public float x = 1;
    public float y = 100;
    private float a;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyBasicAI = this.transform.parent.gameObject.GetComponent(typeof (EnemyBasicAI)) as EnemyBasicAI;
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
            Player.GetComponent<Rigidbody2D>().AddForce(Direction, ForceMode2D.Force);
        }
    }

    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            _playerOnHead = true;
            
            enemyBasicAI.StartCoroutine(enemyBasicAI.ReceiveDamage(1));
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //StartCoroutine(ForceOff());
         _playerOnHead = false;
    }

    IEnumerator ForceOff()
    {
        yield return new WaitForSeconds(0.5f);
        _playerOnHead = false;
    }


}
