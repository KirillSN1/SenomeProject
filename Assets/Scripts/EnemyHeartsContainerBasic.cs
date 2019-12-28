using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeartsContainerBasic : MonoBehaviour
{
    public Sprite[] Hearts;
    public Image Image;

    private int _enemyHealth;

    void Start()
    {

        Image = GetComponent<Image>();
    }


    void Update()
    {

        _enemyHealth = transform.parent.parent.GetComponent<BasicBehavior>().Health;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().Health;

        if (_enemyHealth <= 0)
        {
            _enemyHealth = 0;
        }
        Image.sprite = Hearts[_enemyHealth];

    }
}
