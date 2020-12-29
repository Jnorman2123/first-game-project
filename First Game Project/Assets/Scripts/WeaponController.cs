using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Variable for attack damage
    public float damage = 50;
    // Variable for player game object and player controller
    private GameObject player;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // Set player to the player game object
        player = GameObject.Find("Player");
        // Get player controller component
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //On collision with monster do damage to monster
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Regular Monster") || collision.gameObject.CompareTag("Tank Monster") || collision.gameObject.CompareTag("Fast Monster") && playerController.attackDelay == false)
        {
            collision.gameObject.SendMessage("TakeDamage", damage);
            Debug.Log("hit");
        }
    }
}
