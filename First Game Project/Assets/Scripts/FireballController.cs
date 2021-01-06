using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    // Rigidbody variable for fireball
    private Rigidbody fireballRigidbody;
    // Variable for fireball speed
    public float fireballSpeed = 10.0f;
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
    }

    // Function to move fireball forward
    private void MoveFireball()
    {
        // New vector 3 to set move direction to forward
        Vector3 fireballMovement = Vector3.forward;
        // Move fireball according to speed
        fireballRigidbody.AddForce(fireballMovement * fireballSpeed * Time.deltaTime, ForceMode.Impulse);
    }
}
