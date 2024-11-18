using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveManagerX : MonoBehaviour
{
    public Text instructionsText; 
    public Text winText; 
    public Text loseText; 
    public Text waveText; 
    public SpawnManagerX spawnManager;

    private bool gameActive = false;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        instructionsText.gameObject.SetActive(true);
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        waveText.gameObject.SetActive(false);
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

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }

        if (gameActive && !gameOver)
        {
            CheckWinCondition();
            CheckLoseCondition();
        }
    }

    private void StartGame()
    {
        instructionsText.gameObject.SetActive(false);
        waveText.gameObject.SetActive(true); 
        UpdateWaveText(); 
        Time.timeScale = 1f; 
        gameActive = true;
    }

    private void CheckWinCondition()
    {
        if (spawnManager.waveCount > 10 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            WinGame();
        }
    }

    private void CheckLoseCondition()
    {
        // Count how many soccer balls have reached the player goal
        int playerGoalCount = GameObject.Find("Player Goal").GetComponentsInChildren<EnemyX>().Length;

        // End game if all balls have reached the player goal
        if (playerGoalCount > 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            LoseGame();
        }
    }

    public void OnWaveComplete()
    {
        // Called when a wave is completed (e.g., no enemies left on the field)
        if (spawnManager.waveCount <= 10)
        {
            //spawnManager.waveCount++;
            UpdateWaveText();
        }
    }

    private void UpdateWaveText()
    {
        waveText.text = "Wave: " + spawnManager.waveCount;
    }

    private void WinGame()
    {
        gameOver = true;
        winText.gameObject.SetActive(true);
        winText.text = "You Win! Press R to Restart.";
        Time.timeScale = 0f; // Freeze game
    }

    private void LoseGame()
    {
        gameOver = true;
        loseText.gameObject.SetActive(true);
        loseText.text = "You Lose! Press R to Restart.";
        Time.timeScale = 0f; // Freeze game
    }
}
