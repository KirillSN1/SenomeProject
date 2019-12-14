using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public int Value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameManager.Gm != null)
            {
                Debug.Log("Gm is here");
                GameManager.Gm.Collect(Value, gameObject.tag);     // передаем тег собранного лута
                PlayAudio();
            }
            else
            {
                Debug.Log("cannot found Gm");
            }

            Destroy(gameObject);
        }
    }

    void PlayAudio()
    {
        GetComponentInParent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("CoinsSound/Coin1"));
    }
}
