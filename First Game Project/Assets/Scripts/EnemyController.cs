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
    // Damage delay variable and Enemy attack delay
    private bool damageDelay = false;
    private bool enemyAttackDelay = false;
    // Attack range variable
    private float attackRange;
    // Regular monster weapon hit box variable
    public GameObject regularMonsterHitBox;
  
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
        // Set attack range based on the type of monster
        if (gameObject.CompareTag("Regular Monster"))
        {
            attackRange = 2.0f;
        } else if (gameObject.CompareTag("Tank Monster"))
        {
            attackRange = 3.0f;
        } else if (gameObject.CompareTag("Fast Monster"))
        {
            attackRange = 6.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is started and if so allow enemy to move
        if (spawnManager.gameIsStarted)
        {
            // MoveEnemy();
        }  
    }

    // Fixed update for raycasting
    private void FixedUpdate()
    {
        // Get the player layer mask
        LayerMask playerMask = LayerMask.GetMask("Player");
        // Check to see if the enemy is in range of the player and start enemy attack routine
        if (Physics.Raycast(transform.position, transform.forward, attackRange, playerMask))
        {
            StartCoroutine("EnemyAttack");
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

    // CoRoutine for enemy attack
    IEnumerator EnemyAttack()
    {
        // Call spawn hit box then wait 0.1 seconds and remove the hit box
        if (enemyAttackDelay == false)
        {
            SpawnEnemyHitBox();
            StartCoroutine("EnemyAttackDelay");
            yield return new WaitForSeconds(0.1f);
            RemoveEnemyHitBox();
        }
    }

    // Function to spawn the correct hit box based on monster type
    void SpawnEnemyHitBox()
    {
        // Get enemy position vector3 and rotation
        Vector3 enemyPosition = transform.position;
        Vector3 enemyDirection = transform.forward;
        Quaternion enemyRotation = transform.rotation;
        // Set enemy hit box position based on type of monster
        if (gameObject.CompareTag("Regular Monster"))
        {
            float spawnDistance = 1.3f;
            Vector3 enemyHitBoxPosition = enemyPosition + enemyDirection * spawnDistance;
            // Create new enemy hit box
            Instantiate(regularMonsterHitBox, enemyHitBoxPosition, enemyRotation);
        }
    }

    // Function to remove the enemy hit box
    private void RemoveEnemyHitBox()
    {
        // Find the enemy hit box object
        GameObject enemyHitBoxClone = GameObject.Find("Regular Monster Sword(Clone)");
        Destroy(enemyHitBoxClone);
    }

    // Ienumerator to add a delay to how often the enemy can take damage
    IEnumerator DamageDelay()
    {
        // Set damage delay to true, wait 0.5 seconds and then set damage delay to false
        damageDelay = true;
        yield return new WaitForSeconds(0.5f);
        damageDelay = false;
    }

    // Ienumerator to add a delay to the enemy attack
    IEnumerator EnemyAttackDelay()
    {
        // Set enemy attack delay to true, wait 0.5 seconds and then set enemy attack delay to false
        enemyAttackDelay = true;
        yield return new WaitForSeconds(1.0f);
        enemyAttackDelay = false;
    }
}
