using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    //Variable for sword knock back power 
    private float knockbackPower = 1000.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 swordKnockback = (collision.gameObject.transform.position - transform.position);
        if (!collision.gameObject.CompareTag("Player"))
        {
            enemyRb.AddForce(swordKnockback * knockbackPower, ForceMode.Impulse);
            Debug.Log(enemyRb);
        }   
    }
}
