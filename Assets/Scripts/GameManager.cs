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
    public MobileInput InputClass;

    public Text MainScoreCoinsText;

    public static GameManager Gm;             
    public GameObject Player;

    public enum GameStates { OnMainMenu, Playing, LostLevel, BeatLevel };
    public GameStates GameState = GameStates.OnMainMenu;

    [HideInInspector]
    public bool GameIsOver = false;

    [HideInInspector]
    public string CurrentLevel;

    private PlayerBehaviour _playerState;
    private int _scoreCoins = 0;
    private int CurrentCount;

    void Awake()
    {
        if (Gm == null)
        {
            Gm = gameObject.GetComponent<GameManager>();
        }

        CurrentLevel = SceneManager.GetActiveScene().name;
        MainScoreCoinsText.text = "X  " + _scoreCoins.ToString() + "/" + GameObject.FindGameObjectsWithTag("Coin").Length;

        if (CurrentLevel != MainMenuLevel)
        {
           Player = GameObject.FindGameObjectWithTag("Player");
           _playerState = Player.GetComponent<PlayerBehaviour>();

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
        CurrentLevel = SceneManager.GetActiveScene().name;
        CurrentCount = GameObject.FindGameObjectsWithTag("Coin").Length-1;

        if(CurrentLevel == MainMenuLevel)
        {
            GameState = GameStates.OnMainMenu;
        }

        if (GameState != GameStates.OnMainMenu)
        {
            if(!_playerState.IsAlive)
            {
                GameState = GameStates.LostLevel;
            }
            else if(GameState != GameStates.BeatLevel)
            {
                GameState = GameStates.Playing;
            }
        }

        if (GameState == GameStates.Playing && CanBeatLevel)
        {
            if (_scoreCoins >= BeatLevelScore)
            {
                GameState = GameStates.BeatLevel;
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

    void SetBeatLevelState()         // в случае победы на уровне - загружаем BeatLevelCanvas
    {
        GameIsOver = true;
        StartLevelCanvas.SetActive(false);
        BeatLevelCanvas.SetActive(true);

        var inputPanel = GameObject.FindGameObjectWithTag("InputPanel");

        if(inputPanel)     // проверяем, что панель ввода еще не отключена
        {
            InputClass.RightUp();
            InputClass.LeftUp();
            inputPanel.SetActive(false);
        }
        //Воспроизведение звука победы в Background Music
    }

    public void CloseActivePanel(GameObject activeCanvas)
    {
        Debug.Log("Close " + activeCanvas.name);
        activeCanvas.SetActive(false);
    }

}
