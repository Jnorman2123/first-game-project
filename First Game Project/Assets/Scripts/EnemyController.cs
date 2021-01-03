using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    //Variables for enemy movement
    private float enemySpeed = 2500.0f;
    //Variable for player
    private GameObject player;
    //Variable for enemy rigidbody 
    private Rigidbody enemyRb;
    //Enemy health variable
    public int health;
    // spawn manager variable
    private SpawnManager spawnManager;
    // Health bar and health bar text variables
    public Slider healthBar;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        //Set enemyRb to enemy rigidbody
        enemyRb = GetComponent<Rigidbody>();
        //player to player game object
        player = GameObject.Find("Player");
        // Set spawnManager
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnManager.gameIsStarted)
        {
            MoveEnemy();
        }  
    }

    //Create function to move enemies 
    void MoveEnemy()
    {
        //Create enemy movement vector3 for direction
        Vector3 enemyMovement = (player.transform.position - transform.position).normalized;
        enemyMovement.y = 0;
        enemyRb.AddForce(enemyMovement * enemySpeed * Time.deltaTime, ForceMode.Impulse);
        //Make enemy face the way they are moving
        transform.rotation = Quaternion.LookRotation(enemyMovement);
    }

    // Function to subtract health when taking damage
    void TakeDamage(int damageAmount)
    {
        // Decrease current health by the damage amount
        health -= damageAmount;
        // If the enemies health reaches zero destory the object
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
