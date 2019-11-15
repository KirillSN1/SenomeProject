using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakSpot : MonoBehaviour
{
    public float TimeToReturnToNormal = 1.5f;
    private float _timeLeft;

    private bool _playerOnHead;
    private Vector3 _scale;
    private Vector3 _position;

    void Start()
    {
        _scale = transform.parent.localScale;
        _position = transform.parent.localPosition;
    }

    private void FixedUpdate()
    {
        if (!_playerOnHead)
        {
            _timeLeft -= Time.deltaTime;
            ReturnToNormal();
        }
    }

    public void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            var playerBehaviour = obj.GetComponent<PlayerBehaviour>();

            _playerOnHead = true;
            transform.parent.localScale = new Vector3(_scale.x, _scale.y / 2, _scale.z);
            _timeLeft = TimeToReturnToNormal;

           // obj.gameObject.GetComponent<Rigidbody2D>().velocity = obj.transform.right;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        _playerOnHead = false; 
    }

    private void ReturnToNormal()
    {
        if (_timeLeft <= 0)
        {
            transform.parent.localScale = _scale;
        }
    }
}
