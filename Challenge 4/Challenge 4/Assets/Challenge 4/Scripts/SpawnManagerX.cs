﻿/*
* Name: John Chirayil
* File: SpawnManagerX.cs
* CGE401 - Assignment 7 (Challenge 4)
* Description: Manages when and where the 
* player, enemies, and powerups spawn in-game.
* Also manages wave count.
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public Text loseText;
    public Text waveText;
    public Text winText;
    public Text instructionsText;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public int enemyCount;
    public int waveCount = 1;
    public float enemySpeed = 50;

    public GameObject player;

    private int soccerBallsScoredInPlayerGoal = 0; // Tracks balls scored in player goal
    private int soccerBallsScoredInEnemyGoal = 0; // Tracks balls scored in enemy goal
    private int totalSoccerBallsInWave = 0;       // Total soccer balls in the current wave
    private bool gameActive = false;
    private bool gameOver = false;

    void Start()
    {
        instructionsText.gameObject.SetActive(true);
        winText.gameObject.SetActive(false);
        waveText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        instructionsText.text = "Knock soccer balls into the enemy goal to clear waves.\n" +
                                "Don't let all soccer balls hit your goal, or you lose.\n" +
                                "You can Collect Gems to Enhance Your Bounce Power.\n" +
                                "You can also use the Left Shift Key to Turbo Boost.\n\n" +
                                "Press SPACE to start.";
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameActive && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (gameActive && !gameOver)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (enemyCount == 0)
            {
                if (waveCount < 10)
                {
                    waveCount++;
                    waveText.text = "Wave: " + waveCount;
                    SpawnEnemyWave(waveCount);
                }
                else
                {
                    WinGame();
                }
            }
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

    }

    public void OnSoccerBallScored(bool scoredByPlayer)
    {
        if (scoredByPlayer)
        {
            soccerBallsScoredInEnemyGoal++; // Player scored a goal
        }
        else
        {
            soccerBallsScoredInPlayerGoal++; // soccer ball hits player goal

            // Check if all soccer balls have scored in the player goal
            if (soccerBallsScoredInPlayerGoal == totalSoccerBallsInWave)
            {
                TriggerGameOver();
            }
        }
    }

    private void TriggerGameOver()
    {
        gameOver = true;
        waveText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(true);
        loseText.text = "You Lose! Press R to Restart.";
        Time.timeScale = 0f; // Pause the game
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    public void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        ResetPlayerPosition(); // put player back at start
        enemySpeed += 10;

        // Reset stats for the wave
        totalSoccerBallsInWave = enemiesToSpawn;
        soccerBallsScoredInPlayerGoal = 0;
    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

    private void StartGame()
    {
        instructionsText.gameObject.SetActive(false);
        waveText.gameObject.SetActive(true);
        waveText.text = "Wave: " + waveCount;
        SpawnEnemyWave(waveCount);
        Time.timeScale = 1f;
        gameActive = true;
    }

    private void WinGame()
    {
        gameOver = true;
        winText.gameObject.SetActive(true);
        winText.text = "You Win! Press R to Restart.";
        Time.timeScale = 0f; // Freeze game
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Challenge 4");
        waveCount = 1;
        gameOver = false;
        gameActive = false;
        waveText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        instructionsText.gameObject.SetActive(true);
        Time.timeScale = 0;
        waveText.text = "";
    }
}
