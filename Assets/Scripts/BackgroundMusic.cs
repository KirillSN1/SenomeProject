using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    
    public GameObject BackgroundSourcePrefab;
    public GameManager gameManager;

    public AnimationCurve curve;

    private AudioSource MainAudioSource;

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("MainAudioSource") == null)
        {
            if (gameManager.CurrentScane == "Level1")
            {
                Instantiate(BackgroundSourcePrefab);
                MainAudioSource = GameObject.FindGameObjectWithTag("MainAudioSource").GetComponent<AudioSource>();
                
                MainAudioSource.PlayOneShot(Resources.Load<AudioClip>("BackgroundMusic/Game1"));
                DontDestroyOnLoad(MainAudioSource);
            }
            if (gameManager.CurrentScane == "MainMenu")
            {
                Instantiate(BackgroundSourcePrefab);
                //MainAudioSource.PlayOneShot(Resources.Load<AudioClip>("BackgroundMusic/MainMenu1"));
            }
           // Debug.Log("null");
        }
    }
}
