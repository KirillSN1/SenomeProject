using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Res : MonoBehaviour
{

    public KeyCode RespawnButton;
    public GameObject ResObj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(RespawnButton))
       {
            Instantiate(ResObj,transform.position,Quaternion.identity);
       } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
