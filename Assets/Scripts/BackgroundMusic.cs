using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public GameObject BackgroundSource;
    

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("BackgroundSouce") == null)
        {
            Instantiate(BackgroundSource);
            GameObject.FindGameObjectWithTag("BackgroundSouce").GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("BackgroundMusic/plenka_-_Looking_for"));
        }
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("BackgroundSouce"));
    }

    private void Start()
    {
        
    }


    void Update()
    {
        
    }
}
