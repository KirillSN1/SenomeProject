using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public int ScoreCoins = 0;
    public Text MainScoreCoins;

    private PlayerBehaviour playerState;

   // private bool _playerIsAlive;

    
    void Start()
    {
        playerState = player.GetComponent<PlayerBehaviour>();

        if (Gm == null)
        {
            Gm = gameObject.GetComponent<GameManager>();
        }
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

    public void Collect(int amount, string tag)
    {
        if(tag == "Coin")
        {
            ScoreCoins += amount;
            MainScoreCoins.text = "X  " + ScoreCoins.ToString();
            Debug.Log("Final Score: " + ScoreCoins);
        }


        //switch (tag)
        //{
        //    case "Coin":
        //        ScoreCoins += amount;
        //        MainScoreCoins.text = "X  " + ScoreCoins.ToString();
        //        Debug.Log("Final Score: " + ScoreCoins);

        //        break;

        //    case "Gem":
        //        ScoreGems += amount;
        //        MainScoreGems.text = "X  " + ScoreGems.ToString();
        //        Debug.Log("Final Score: " + ScoreGems);

        //        break;
        //}

    }
}
