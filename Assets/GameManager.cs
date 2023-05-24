using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public static Transform currentShop;
    public GameObject gameOverUI;
    public GameObject gameIsWon;
    
    void Update()
    {

        if (PlayerStats.Lose == true)
        {
            EndGame();
        }
        else if (PlayerStats.Wictory == true)
        {
            GameWon();
        }
    }

    void EndGame()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
    
    void GameWon()
    {
        gameIsWon.SetActive(true);
        Time.timeScale = 0f;
    }
}
