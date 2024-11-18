/*
* Name: John Chirayil
* File: EnemyX.cs
* CGE401 - Assignment 7 (Challenge 4)
* Description: Controls the enemy AI in game.
*/

using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private SpawnManagerX spawnManagerX;
    private LossConditionX lossConditionX;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");
        spawnManagerX = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();
        lossConditionX = GameObject.Find("Loss Condition").GetComponent<LossConditionX>();
        speed = spawnManagerX.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            lossConditionX.OnSoccerBallScored(true); // Ball scored in Enemy Goal
            Destroy(gameObject);
        }
        else if (other.gameObject.name == "Player Goal")
        {
            lossConditionX.OnSoccerBallScored(false); // Ball scored in Player Goal
            Destroy(gameObject);
        }

    }

}
