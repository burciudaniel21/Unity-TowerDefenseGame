using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverUI;

    private void Start()
    {
        gameOver = false;
    }

    void Update()
    {
        if(PlayerStats.Lives <= 0)
        {
            if(gameOver)
            {
                return;
            }
            if(PlayerStats.Lives <= 0)
            {
                EndGame();
            }
        }

        if (Input.GetKeyDown("r"))
        {
            EndGame();   
        }    
    }

    private void EndGame()
    {
        gameOverUI.SetActive(true);
        gameOver = true;

    }
}
