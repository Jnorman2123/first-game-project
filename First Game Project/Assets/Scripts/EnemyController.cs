using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Variables for enemy movement
    private float enemySpeed = 2500.0f;
    //Variable for player
    private GameObject player;
    //Variable for enemy rigidbody 
    private Rigidbody enemyRb;
    // Start is called before the first frame update
    void Start()
    {
        //Set enemyRb to enemy rigidbody
        enemyRb = GetComponent<Rigidbody>();
        //player to player game object
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
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
}
