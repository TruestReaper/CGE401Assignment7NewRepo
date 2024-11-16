/*
* Name: John Chirayil
* File: RotateCamera.cs
* CGE401 - Assignment 7 (Prototype 4)
* Description: Rotates the camera 
* surrounding the sphere.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
