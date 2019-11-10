using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public string startGameLevel = "Level1";
    public string nextLevel = "Level1";
    public string levelToRestart = "Level1";
    public string levelWithArena = "Arena";


    public void StartGame()
    {
        Debug.Log("Starting the game!");
        SceneManager.LoadScene(startGameLevel);
    }

    public void OpenTip()
    {
        Debug.Log("We don't have tips yet!");
    }   
    
    public void LoadNextLevel()
    {
        Debug.Log("Loading next level");
        SceneManager.LoadScene(nextLevel);
    }
     public void LoadArena()
    {
        // loading level with arena
        Debug.Log("We don't have Arena yet!");
      //  SceneManager.LoadScene(levelWithArena);
    }

    public void RestartLevel()
    {
        Debug.Log("Restart level");
        SceneManager.LoadScene(levelToRestart);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    
}
