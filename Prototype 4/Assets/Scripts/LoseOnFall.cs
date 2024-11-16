/*
* Name: John Chirayil
* File: LoseOnFall.cs
* CGE401 - Assignment 7 (Prototype 4)
* Description: This code will allow the player 
* to lose the game after falling off the platform.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseOnFall : MonoBehaviour
{
    public float fallThreshold = -10f; 
    public Text loseText;               
    private bool isGameOver = false; 

    // Start is called before the first frame update
    void Start()
    {
        // Hide the lose text once game starts
        loseText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fallThreshold && !isGameOver)
        {
            GameOver();
        }

        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    void GameOver()
    {
        loseText.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isGameOver = true;
    }

    void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Prototype 4 Finished");
    }
}
