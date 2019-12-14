using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    private EnemyBasicAI Enemy;
    private bool Spawn=true;
    private Transform CoinSource;

    public int NumCoins;
    public float timeout;
    public GameObject CoinPrefab;


    void Awake()
    {
        Enemy = GetComponent<EnemyBasicAI>();
        CoinSource = GameObject.FindGameObjectWithTag("CoinSource").transform;
    }

    void Update()
    {
        if (Enemy.Health==0 && Spawn)
        {
            new WaitForSeconds(01);
            Instantiate(CoinPrefab, gameObject.transform.position, Quaternion.identity,CoinSource);
            Spawn = false;
        }
    }
}
