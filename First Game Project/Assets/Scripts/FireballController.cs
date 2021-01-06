using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    // Rigidbody variable for fireball
    private Rigidbody fireballRigidbody;
    // Variable for fireball speed and damage
    private float fireballSpeed = 20.0f;
    private float fireballDamage = 10.0f;
    // Variable for fireball life span
    private float fireballLifeSpan = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set fireball rigidbody
        fireballRigidbody = GetComponent<Rigidbody>();
        // Fireball should only collide with the player
        Physics.IgnoreLayerCollision(0, 7);
    }

    // Update is called once per frame
    void Update()
    {
        MoveFireball();
        StartCoroutine("DestroyFireball");
    }

    // Function to move fireball forward
    private void MoveFireball()
    {
        // New vector 3 to set move direction to forward
        Vector3 fireballMovement = Vector3.forward;
        // Move fireball according to speed
        fireballRigidbody.AddForce(fireballMovement * fireballSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    // Ienumerator to despawn fireball after a set time
    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(fireballLifeSpan);
        Destroy(gameObject);
    }

    // On collision with player do damage
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("PlayerDamaged", fireballDamage);
        }
    }
}
