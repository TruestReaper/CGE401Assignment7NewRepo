﻿/*
* Name: John Chirayil
* File: RotateCameraX.cs
* CGE401 - Assignment 7 (Challenge 4)
* Description: Allows the player to rotate
* their camera in game.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    private float speed = 150;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);

        transform.position = player.transform.position; // Move focal point with player

    }
}
