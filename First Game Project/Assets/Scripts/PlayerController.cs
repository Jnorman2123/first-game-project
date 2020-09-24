using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables for character movement
    public float forwardInput;
    public float horizontalInput;
    public float speed = 10.0f;
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
        //Call move player function to move player
        MovePlayer();
    }

    //Method to move character based on user input
    private void MovePlayer()
    {
        //Set inputs to vertical and horizontal input
        forwardInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Set movement variable to the player inputs
        Vector3 movement = new Vector3(horizontalInput, 0.0f, forwardInput); 

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        //Move character forward 
        
        //transform.Translate(Vector3.forward * forwardInput * speed * Time.deltaTime);
    }
}
