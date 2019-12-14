using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public GameObject BackgroundSourcePrefab;
    public GameManager gameManager;
    public AnimationCurve curve;
    public AudioSource MainAudioSource;

    public bool GameMusic;
    public bool Victory;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();

        if (GameObject.FindGameObjectWithTag("MainAudioSource") == null)
        {
            if (GameManager.Gm.CurrentLevel == GameManager.Gm.FirstLevel)
            {
                Instantiate(BackgroundSourcePrefab);
                MainAudioSource = GameObject.FindGameObjectWithTag("MainAudioSource").GetComponent<AudioSource>();

                MainAudioSource.PlayOneShot(Resources.Load<AudioClip>("BackgroundMusic/Game1"));
                DontDestroyOnLoad(MainAudioSource);

                GameMusic = true;
            }
            if (GameManager.Gm.CurrentLevel == GameManager.Gm.MainMenuLevel)
            {
                Instantiate(BackgroundSourcePrefab);
                //MainAudioSource.PlayOneShot(Resources.Load<AudioClip>("BackgroundMusic/MainMenu1"));
            }

            //if (gameManager.CurrentScane == "Level1")
            //{
            //    Instantiate(BackgroundSourcePrefab);
            //    MainAudioSource = GameObject.FindGameObjectWithTag("MainAudioSource").GetComponent<AudioSource>();

            //    MainAudioSource.PlayOneShot(Resources.Load<AudioClip>("BackgroundMusic/Game1"));
            //    DontDestroyOnLoad(MainAudioSource);
            //}
            //if (gameManager.CurrentScane == "MainMenu")
            //{
            //    Instantiate(BackgroundSourcePrefab);
            //    //MainAudioSource.PlayOneShot(Resources.Load<AudioClip>("BackgroundMusic/MainMenu1"));
            //}
        }

        
    }
    void Update()
    {
        VictorySound();
    }

    void VictorySound()
    {
        if (gameManager.GameState == GameManager.GameStates.BeatLevel)
        {
            if (GameMusic)
            {
                MainAudioSource.Stop();
                GameMusic = false;
                Victory = true;
            } 
        }
        if (Victory)
            {
                MainAudioSource.PlayOneShot(Resources.Load<AudioClip>("BackgroundMusic/Victory0")); //Звук победы
                Victory = false;
            }

    }

}
