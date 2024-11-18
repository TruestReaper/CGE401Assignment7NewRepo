/*
* Name: John Chirayil
* File: LossConditionX.cs
* CGE401 - Assignment 7 (Challenge 4)
* Description: This script will affect the loss
* condition of this game.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LossConditionX : MonoBehaviour
{
    public Text loseText;
    public Text waveText;
    //public GameObject playerGoal; 
    //public GameObject enemyGoal;

    //private int totalSoccerBalls;
    //private int soccerBallsInPlayerGoal = 0;
    //private bool playerScored = false; // Flag to track if the player scored
    private int soccerBallsScored = 0;
    private int playerScore = 0;
    private int totalBallsInWave = 0;
    private bool gameOver = false;
    private SpawnManagerX spawnManagerX;

    // Start is called before the first frame update
    void Start()
    {
        loseText.gameObject.SetActive(false);
        spawnManagerX = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void OnSoccerBallScored(bool scoredByPlayer)
    {
        if (scoredByPlayer)
        {
            playerScore++; // Player scored a goal
        }
        else
        {
            soccerBallsScored++; // soccer ball hits player goal

            // Check if all soccer balls have scored in the player goal
            if (soccerBallsScored == spawnManagerX.enemyCount)
            {
                TriggerGameOver();
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject.transform.position.z == playerGoal.transform.position.z)
            {
                soccerBallsInPlayerGoal++;
            }

            // Remove the soccer ball from the game
            Destroy(other.gameObject);

            // Check if all soccer balls have hit the player goal
            if (soccerBallsInPlayerGoal == totalSoccerBalls)
            {
                TriggerGameOver();
            }
        }
        // Check if the ball hit the enemy goal
        //else if (other.gameObject.transform.position.z == enemyGoal.transform.position.z)
        //{
          //  playerScored = true;
            //Destroy(other.gameObject); 
        //}
    }*/

    private void TriggerGameOver()
    {
        gameOver = true;
        waveText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(true);
        loseText.text = "You Lose! Press R to Restart.";
        Time.timeScale = 0f; // Pause the game
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Challenge 4");
    }

    public void ResetWaveStats(int totalBalls)
    {
        totalBallsInWave = totalBalls;
        soccerBallsScored = 0;
        playerScore = 0;
    }
}
