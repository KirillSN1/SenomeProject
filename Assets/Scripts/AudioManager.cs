using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private PlayerBehaviour Player;

    [Header("For Player")]
    public AudioClip Attack;
    public AudioClip Run;
    public AudioClip Hit;
    public AudioClip TakeCoin;
    public AudioSource PlayerSource;

    
    

    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }

    
    void Update()
    {
        
    }
    public void PlayClipEvent()
    {
        PlayerSource.PlayOneShot(Run);
    }

   

    


    
}
