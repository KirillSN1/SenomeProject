using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    private bool _pause = false;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject PlayPanel;
    public void Pause()
    {
        if(!_pause) // pause on
        {
            _pause  = true;
            MainPause();
        }
    }
    public void Resume()
    {
        _pause = false;
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        PlayPanel.SetActive(true);
    }
    private void MainPause()
    {
        PausePanel.SetActive(true);
        PlayPanel.SetActive(false);
        Time.timeScale = 0f;
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
    private void OnDisable() {
        Time.timeScale = 1f;
    }
}
