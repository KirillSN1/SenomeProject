using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHepler : MonoBehaviour
{
    [SerializeField] private GameObject menuMusic;
    void Start()
    {
     Destroy(GameObject.Find("MainAudioSource(Clone)"));
     if(!GameObject.Find("MenuAudioSource(Clone)"))
     {
         Instantiate(menuMusic,transform.position,Quaternion.identity);
     }
    }
}
