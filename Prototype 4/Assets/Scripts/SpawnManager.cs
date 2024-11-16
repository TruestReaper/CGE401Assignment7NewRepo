/*
* Name: John Chirayil
* File: SpawnManager.cs
* CGE401 - Assignment 7 (Prototype 4)
* Description: Manages the enemies spawning in game.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    public Text waveText;
    public Text messageText;
    public Text winText;
    private bool gameStarted = false;
    private bool gameWon = false;

    // Start is called before the first frame update
    void Start()
    {
        ShowMessage("Welcome to Ball Survival Island!\n\nSurvive 10 Waves to Win!\nKnock off all the balls to progress!\nYou will lose if you fall off the platform!\nCollect Gems to Increase Bounce Power!\n\nPress SPACE to Start!", true);
        Time.timeScale = 0;
        waveText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Instantiate the enemy in the random position
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerUp(int powerupsToSpawn)
    {
        for (int i = 0; i < powerupsToSpawn; i++)
        {
            // Instantiate the enemy in the random position
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        // generate random position on platform
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    private void WinGame()
    {
        gameWon = true;
        winText.text = "You Win! Press R to Try Again!";
        winText.gameObject.SetActive(true);
        waveText.gameObject.SetActive(false);
        Time.timeScale = 0; // Freeze game
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (gameStarted && !gameWon)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (enemyCount == 0)
            {
                if (waveNumber < 10)
                {
                    waveNumber++;
                    waveText.text = "Wave: " + waveNumber;
                    SpawnEnemyWave(waveNumber);
                    SpawnPowerUp(1);
                }
                else
                {
                    WinGame();
                }
            }
        }
        if (gameWon && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    private void StartGame()
    {
        gameStarted = true;
        ShowMessage("", false);
        waveText.gameObject.SetActive(true);
        Time.timeScale = 1; 
        waveText.text = "Wave: " + waveNumber;
        SpawnEnemyWave(waveNumber);
        SpawnPowerUp(1);
    }


    private void RestartGame()
    {
        SceneManager.LoadScene("Prototype 4 Finished");
        waveNumber = 1;
        gameWon = false;
        gameStarted = false;
        waveText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        ShowMessage("Welcome to Ball Survival Island!\n\nSurvive 10 Waves to Win!\nKnock off all the balls to progress!\nYou will lose if you fall off the platform!\nCollect Gems to Increase Bounce Power!\n\nPress SPACE to Start!", true);
        Time.timeScale = 0;
        waveText.text = "";
    }

    private void ShowMessage(string message, bool show)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(show);
    }
}
