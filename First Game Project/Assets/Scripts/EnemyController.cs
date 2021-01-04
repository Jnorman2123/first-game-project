using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    //Variables for enemy movement
    private float enemySpeed = 2000.0f;
    //Variable for player
    private GameObject player;
    //Variable for enemy rigidbody 
    private Rigidbody enemyRb;
    //Enemy health variables
    public float maxHealth;
    private float currentHealth;
    // health bar variable
    public HealthBar healthBar;
    // spawn manager variable
    private SpawnManager spawnManager;
    // Damage delay variable
    private bool damageDelay = false;
  
    // Start is called before the first frame update
    void Start()
    {
        //Set enemyRb to enemy rigidbody
        enemyRb = GetComponent<Rigidbody>();
        // Set enemy health
        currentHealth = maxHealth;
        // Set health bar to max health
        healthBar.SetMaxHealth(maxHealth);
        //player to player game object
        player = GameObject.Find("Player");
        // Set spawnManager
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is started and if so allow enemy to move
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
        // Check if damage delay is false
        if (damageDelay == false)
        {
            // Decrease current health by the damage amount and decrease health on health bar and start the damage delay coroutine
            currentHealth -= damageAmount;
            healthBar.SetHealth(currentHealth);
            StartCoroutine("DamageDelay");
            // If the enemies health reaches zero destory the object
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }   
    }

    // Ienumerator to add a delay to how often the enemy can take damage
    IEnumerator DamageDelay()
    {
        // Set damage delay to true, wait 0.5 seconds and then set damage delay to false
        damageDelay = true;
        yield return new WaitForSeconds(0.5f);
        damageDelay = false;
    }
}
