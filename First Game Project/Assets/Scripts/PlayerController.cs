using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variable for player start position
    private Vector3 startPosition = new Vector3(0, 0.65f, 0);
    //Variables for character movement
    private float forwardInput;
    private float horizontalInput;
    [SerializeField] float turnSpeed;
    private float turnPlayer = 0.0f;
    public float moveSpeed = 12.5f;
    //Vatiables for player boosts
    private float speedBoost = 1.5f;
    private float damageBoost = 2.0f;
    //Variables for monster knock back power
    public float tankMonsterPower = 2000.0f;
    public float monsterPower = 1000.0f;
    public float fastMonsterPower = 500.0f;
    //Variables for monster attack power
    public float tankMonsterAttack = 50.0f;
    public float monsterAttack = 25.0f;
    public float fastMonsterAttack = 12.5f;
    //Player rigidbody and renderer variables
    private Rigidbody playerRb;
    private Renderer playerRenderer;
    //Sword game object variable and weapon controller script
    public GameObject sword;
    private WeaponController weaponController;
    //Variables for player health
    public float maxHealth = 250;
    public float currentHealth;
    //Variables for materials
    public Material normalMaterial;
    public Material damageMaterial;
    public Material speedMaterial;
    // Variable for spawn manager
    private SpawnManager spawnManager;
    // Variable for health bar
    public HealthBar healthBar;
    // Variable for attack delay
    public bool attackDelay = true;
    

    // Start is called before the first frame update
    void Start()
    {
        //Set weapon controller
        weaponController = sword.GetComponent<WeaponController>();
        //Set playerRb to player rigidbody component
        playerRb = GetComponent<Rigidbody>();
        //Set playerMaterial
        playerRenderer = GetComponent<Renderer>();
        // Set playerManager
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        // Set currentHealth to maxHealth
        currentHealth = maxHealth;
        // Set health bar to max health
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Call move player function to move player if the game is started
        if (spawnManager.gameIsStarted)
        {
            MovePlayer();
            //Call attack function to spawn a hit box when space bar is pressed
            if (Input.GetKeyDown(KeyCode.Space) && attackDelay)
            {
                StartCoroutine("Attack");
                StartCoroutine("DelayAttack");
            }
        }    
    }

    //Method to move character based on user input
    private void MovePlayer()
    {
        //Set inputs to vertical and horizontal input
        forwardInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Set movement variable to the player inputs
        Vector3 movement = new Vector3(0.0f, 0.0f, forwardInput);
        //Move character in direction of input
        transform.Translate(movement * Time.deltaTime * moveSpeed);
        // Turn character based on user horizontal input
        turnPlayer += horizontalInput * turnSpeed * Time.deltaTime; 
        transform.eulerAngles = new Vector3(0.0f, turnPlayer, 0.0f);     
    }
    //Function to spawn a hit box when the player presses the space bar
    /*private void SpawnHitBox()
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
    }*/
    //Function to destroy the hit box
    /*private void RemoveHitBox()
    {
        GameObject hitBoxClone = GameObject.Find("Hit Box(Clone)");
        Destroy(hitBoxClone);
    }*/
    //When player collides with monster the monster will knock the player away
    private void OnCollisionEnter(Collision collision)
    {
        //For each type of monster call Damage function with correct knock back power and damage to player health
        if (collision.gameObject.CompareTag("Tank Monster"))
        {
            Damage(collision.gameObject, tankMonsterPower, tankMonsterAttack);
        } else if (collision.gameObject.CompareTag("Regular Monster"))
        {
            Damage(collision.gameObject, monsterPower, monsterAttack);
        } else if (collision.gameObject.CompareTag("Fast Monster"))
        {
            Damage(collision.gameObject, fastMonsterPower, fastMonsterAttack);
        }
    }
    //Method to determine knock back power based on monster type
    void Damage(GameObject collision, float monsterPower, float monsterAttack)
    {
        //Knock the player away from the monster
        Vector3 awayFromMonster = (transform.position - collision.gameObject.transform.position);
        playerRb.AddForce(awayFromMonster * monsterPower, ForceMode.Impulse);
        //Damage the player
        currentHealth -= monsterAttack;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            transform.position = startPosition;
            spawnManager.Death();
        }
    }
    //Destroy power up when the player collides with it
    private void OnTriggerEnter(Collider other)
    {
        //If health pick up heal the player
        if (other.CompareTag("Health Potion"))
        {
            //Heal player by 50 but dont excede initial health of 250
            currentHealth += 50;
            healthBar.SetHealth(currentHealth);
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
                healthBar.SetHealth(currentHealth);
            }
        } else if (other.CompareTag("Damage Boost"))
        {
            StartCoroutine("DamageBoost");
        } else if (other.CompareTag("Speed Boost"))
        {
            StartCoroutine("SpeedBoost");
        }
        //Destroy the pick up object
        Destroy(other.gameObject);
    }
    //Spawn hit box wait split second and then despawn hitbox
    IEnumerator Attack()
    {
        //SpawnHitBox();
        weaponController.swordAnim.Play("Sword_Attack");
        yield return new WaitForSeconds(0.1f);
        //RemoveHitBox();
    }

    // Add delay between attacks
    IEnumerator DelayAttack()
    {
        attackDelay = false;
        yield return new WaitForSeconds(0.25f);
        attackDelay = true;
    }

    //Double the damage of the players attack and change the player color to red
    IEnumerator DamageBoost()
    {
        //Weapon damage is doubled and player changes color to red
        weaponController.damage *= damageBoost;
        playerRenderer.material = damageMaterial;
        yield return new WaitForSeconds(5.0f);
        //After 5 seconds damage and player color goes back to normal
        weaponController.damage /= damageBoost;
        playerRenderer.material = normalMaterial;
    }
    //Increase the speed of the players movement and change player color to yellow
    IEnumerator SpeedBoost()
    {
        //Speed is increased by speed boost and color changes to yellow
        moveSpeed *= speedBoost;
        playerRenderer.material = speedMaterial;
        yield return new WaitForSeconds(5.0f);
        //After 5 seconds speed and player color goes back to normal
        moveSpeed /= speedBoost;
        playerRenderer.material = normalMaterial;
    }
}
