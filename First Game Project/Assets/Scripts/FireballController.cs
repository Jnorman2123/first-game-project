using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    // Rigidbody variable for fireball
    private Rigidbody fireballRigidbody;
    // Variable for fireball speed
    private float fireballSpeed = 20.0f;
    // Variable for fireball life span
    private float fireballLifeSpan = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set fireball rigidbody
        fireballRigidbody = GetComponent<Rigidbody>();
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
}
