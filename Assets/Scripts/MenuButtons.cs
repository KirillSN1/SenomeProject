using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartNewGame()
    {
        Debug.Log("Starting the game!");
        SceneManager.LoadScene(GameManager.Gm.FirstLevel);
    }

    public void OpenTip()
    {
        Debug.Log("We don't have tips yet!");
    }   
    
    public void LoadNextLevel()
    {
        Debug.Log("Loading next level");
        SceneManager.LoadScene(GameManager.Gm.NextLevel);
    }
   
    public void LoadMainMenu()
    {
        Debug.Log("Restart level");
        SceneManager.LoadScene(GameManager.Gm.MainMenuLevel);
        Destroy(GameObject.FindGameObjectWithTag("MainAudioSource"));
    }

    public void LoadArena()
    {
        Debug.Log("We don't have Arena yet!");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    
}
