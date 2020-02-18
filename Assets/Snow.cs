using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    public GameObject RabbitPlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //transform.Translate(new Vector3(RabbitPlayer.transform.position.x,34,-45));
        gameObject.transform.position = new Vector3(RabbitPlayer.transform.position.x, 34, -45);


    }
}
