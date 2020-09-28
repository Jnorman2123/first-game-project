using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables for character movement
    public float forwardInput;
    public float horizontalInput;
    public float speed = 75.0f;
    //Variables for monster knock back power
    public float tankMonsterPower = 2000.0f;
    public float monsterPower = 1000.0f;
    public float fastMonsterPower = 500.0f;
    //Player rigidbody variable 
    private Rigidbody playerRb;
    //Hit box game object variable
    public GameObject hitBox;

    // Start is called before the first frame update
    void Start()
    {
        //Set playerRb to player rigidbody component
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Call move player function to move player
        MovePlayer();
        //Call attack function to spawn a hit box when space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Attack");
        }   
    }

    //Method to move character based on user input
    private void MovePlayer()
    {
        //Set inputs to vertical and horizontal input
        forwardInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Set movement variable to the player inputs
        Vector3 movement = new Vector3(horizontalInput, 0.0f, forwardInput); 
        //Move character in direction of input
        playerRb.AddForce(movement * speed * Time.deltaTime, ForceMode.Impulse);
        //Make character automatically face the direction it is moving, lock rotation if no input
        if(horizontalInput != 0 || forwardInput != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        } else if (horizontalInput == 0 && forwardInput ==0)
        {
            playerRb.freezeRotation = true;
        }
       
    }
    //Function to spawn a hit box when the player presses the space bar
    private void SpawnHitBox()
    {
        //Get player position vector3 and rotation
        Vector3 playerPosition = transform.position;
        Vector3 playerDirection = transform.forward;
        Quaternion playerRotation = transform.rotation;
        float spawnDistance = 1.0f;
        //Set hit box spawn
        Vector3 hitBoxPosition = playerPosition + playerDirection * spawnDistance;
        //Create new weapon
        Instantiate(hitBox, hitBoxPosition, playerRotation);
    }
    //Function to destroy the hit box
    private void RemoveHitBox()
    {
        GameObject hitBoxClone = GameObject.Find("Hit Box(Clone)");
        Destroy(hitBoxClone);
    }
    //When player collides with monster the monster will knock the player away
    private void OnCollisionEnter(Collision collision)
    {
        //For each type of monster call KnockAway function with correct knock back power
        if (collision.gameObject.CompareTag("Tank Monster"))
        {
            KnockAway(collision.gameObject, tankMonsterPower);
        } else if (collision.gameObject.CompareTag("Regular Monster"))
        {
            KnockAway(collision.gameObject, monsterPower);
        } else if (collision.gameObject.CompareTag("Fast Monster"))
        {
            KnockAway(collision.gameObject, fastMonsterPower);
        }
    }
    //Method to determine knock back power based on monster type
    void KnockAway(GameObject collision, float monsterPower)
    {
        //Knock the player away from the monster
        Vector3 awayFromMonster = (transform.position - collision.gameObject.transform.position);
        playerRb.AddForce(awayFromMonster * monsterPower, ForceMode.Impulse);
    }
    //Destroy power up when the player collides with it
    private void OnTriggerEnter(Collider other)
    {
        //Destroy the pick up object
        Destroy(other.gameObject);
    }

    IEnumerator Attack()
    {
        SpawnHitBox();
        yield return new WaitForSeconds(0.1f);
        RemoveHitBox();
    }
}
