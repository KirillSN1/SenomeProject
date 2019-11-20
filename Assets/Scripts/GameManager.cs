using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{  
    [Header("Level Headers")]
    public string MainMenuLevel = "StartMenu";
    public string FirstLevel = "Level1";
    public string NextLevel;

    [Header("Level Settings")]
    public bool CanBeatLevel = false;
    public int BeatLevelScore = 0;
    public GameObject MainCanvas;
    public GameObject StartLevelCanvas;
    public GameObject BeatLevelCanvas;

    public Text MainScoreCoinsText;

    public static GameManager Gm;             
    public GameObject Player;

    public enum GameStates { OnMainMenu, StartLevel, LostLevel, BeatLevel };
    public GameStates GameState = GameStates.OnMainMenu;

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

           GameState = GameStates.StartLevel;

            if (StartLevelCanvas)
            {
                StartLevelCanvas.SetActive(true);
            }

            if (CanBeatLevel)
            {
                BeatLevelCanvas.SetActive(false);
            }

            if(BeatLevelScore == 0)
            {
                BeatLevelScore = GameObject.FindGameObjectsWithTag("Coin").Length;
            }
        }
        else
        {
           GameState = GameStates.OnMainMenu;
        }       
    }

    void Update()
    {
        if(GameState != GameStates.OnMainMenu && !_playerState.IsAlive)
        {
           GameState = GameStates.LostLevel;
        }

        if (!GameIsOver)
        {
            if (CanBeatLevel && (_scoreCoins >= BeatLevelScore))
            {  
                SetBeatLevelState();
            }
        }

        switch (GameState)                                      
        {
            case GameStates.LostLevel:                     // в случае смерти игрока, перезагружаем текущий уровень
                SceneManager.LoadScene(CurrentLevel);
                break;

            case GameStates.BeatLevel:                 
                SetBeatLevelState();
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

    void SetStartLevelState()
    {
        StartLevelCanvas.SetActive(true);
    }

    void SetBeatLevelState()         // в случае победы на уровне - загружаем BeatLevelCanvas
    {
        GameIsOver = true;
        StartLevelCanvas.SetActive(false);
        BeatLevelCanvas.SetActive(true);

        var inputPanel = GameObject.FindGameObjectWithTag("InputPanel");
        inputPanel.SetActive(false); 
    }

    public void CloseActivePanel(GameObject activeCanvas)
    {
        Debug.Log("Close " + activeCanvas.name);
        activeCanvas.SetActive(false);
    }

}
