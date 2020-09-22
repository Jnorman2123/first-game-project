﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables for character movement
    public float forwardInput;
    public float horizontalInput;
    public float speed = 10.0f;
    //Variables for boundaries
    private float leftBound = 20;
    private float topBound = 10;
    //Player rigidbody variable 
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Turn character side to side
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        //Move character forward 
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * forwardInput * speed * Time.deltaTime);

        //Keep the player in bounds 
        if (transform.position.x > leftBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        } else if (transform.position.x < -leftBound)
        {
            transform.position = new Vector3(-leftBound , transform.position.y, transform.position.z);
        } else if (transform.position.z > topBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topBound);
        } else if (transform.position.z < -topBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -topBound);
        }
    }
}
