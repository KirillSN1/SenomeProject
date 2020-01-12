using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public int Value = 1;
    [HideInInspector]
    public GameObject player;
    private PlayerBehaviour playerBehaviour;
    [HideInInspector]
    public bool flyToTarget = false;
   
    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
    }
    public void FlyToTarget()
    {
        transform.position = Vector2.Lerp(transform.position, player.transform.position, 2f*Time.deltaTime);
    }
    public void Update()
    {
        if (flyToTarget)
            FlyToTarget();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameManager.Gm != null)
            {
                Debug.Log("Gm is here");
                GameManager.Gm.Collect(Value, gameObject.tag);     // передаем тег собранного лута
                //PlayAudio();
            }
            else
            {
                Debug.Log("cannot found Gm");
            }

            Destroy(gameObject);
        }
    }

    public void OnBecameVisible() {
    playerBehaviour.GameObjectsinView.Add(gameObject);

    }
    public void OnBecameInvisible() {
    playerBehaviour.GameObjectsinView.Remove(gameObject);        
    }
    
    void PlayAudio()
    {
        GetComponentInParent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("CoinsSound/Coin1"));
    }
}
