using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [Header("Level Headers")]
    public string MainMenuLevel = "StartMenu";
    public string StartGameLevel = "Level1";
    public string NextLevel = "Level1";

    [Header("Level Settings")]
    public bool CanBeatLevel = false;
    public Text MainScoreCoinsText;

    public static GameManager Gm;
    public GameObject Player;

    public enum GameStates { OnStartMenu, Playing, Death, BeatLevel };
    public GameStates GameState = GameStates.Playing;

    [HideInInspector]
    public bool GameIsOver;

    [HideInInspector]
    public string CurrentLevel;

    private PlayerBehaviour _playerState;
    private int _scoreCoins = 0;

    void Awake()
    {
        if (Gm == null)
        {
            Gm = gameObject.GetComponent<GameManager>();
        }

        CurrentLevel = SceneManager.GetActiveScene().name;

        if(CurrentLevel != MainMenuLevel)
        {
           Player = GameObject.FindGameObjectWithTag("Player");
           _playerState = Player.GetComponent<PlayerBehaviour>();

            GameState = GameStates.Playing;
        }
        else
        {
            GameState = GameStates.OnStartMenu;
        }

    }

    void Update()
    {
        if(GameState != GameStates.OnStartMenu && !_playerState.IsAlive)
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

    public void Collect(int amount, string tag)
    {
        if(tag == "Coin")
        {
            _scoreCoins += amount;
            MainScoreCoinsText.text = "X  " + _scoreCoins.ToString();
            Debug.Log("Final Score: " + _scoreCoins);
        }
    }
}
