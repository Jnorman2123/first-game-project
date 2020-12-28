using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Variable for attack damage
    public float damage = 50;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //On collision with monster do damage to monster
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Regular Monster") || collision.gameObject.CompareTag("Tank Monster") || collision.gameObject.CompareTag("Fast Monster"))
        {
            collision.gameObject.SendMessage("TakeDamage", damage);
        }
    }
}
