using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject PlayPanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject Player;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            Win();
        }
    }
    private void Win()
    {
        PlayPanel.SetActive(false);
        WinPanel.SetActive(true);
        Player.SetActive(false);
    }
}
