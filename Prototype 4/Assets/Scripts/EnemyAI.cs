﻿/*
* Name: John Chirayil
* File: EnemyAI.cs
* CGE401 - Assignment 7 (Prototype 4)
* Description: Controls enemy AI in game.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody enemyRb;
    public GameObject player;
    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Add force toward the direction of the player to the enemy

        // Vector for direction from enemy to player
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        // Add force toward player
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
