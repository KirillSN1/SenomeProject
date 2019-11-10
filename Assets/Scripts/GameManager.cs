using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Gm;

    public GameObject player;

    public enum GameStates { Playing, Death, BeatLevel };
    public GameStates GameState = GameStates.Playing;

    public string CurrentLevel = "SampleScene";
    public string NextLevel = "SampleScene";

    public bool GameIsOver;

    private PlayerBehaviour playerState;

   // private bool _playerIsAlive;

    
    void Start()
    {
        playerState = player.GetComponent<PlayerBehaviour>();
    }


    void Update()
    {
        if(!playerState.IsAlive)
        {
            GameState = GameStates.Death;
        }

        switch(GameState)                                      
        {
            case GameStates.Death:                     // в случае смерти игрока, перезагружаем текущий уровень
                SceneManager.LoadScene(CurrentLevel);
                break;

            case GameStates.BeatLevel:                 // в случае победы на уровне - загружаем следущий
                SceneManager.LoadScene(NextLevel);
                break;
        }

    }
}
