using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Variables for attack damage
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Player Weapon"))
        {
            damage = 50;
        } else if (gameObject.CompareTag("Regular Monster Sword"))
        {
            damage = 25;
        } else if (gameObject.CompareTag("Tank Monster Club"))
        {
            damage = 50;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //On collision with monster do damage to monster
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Player Weapon"))
        {
            if (collision.gameObject.CompareTag("Regular Monster") || collision.gameObject.CompareTag("Tank Monster") || collision.gameObject.CompareTag("Fast Monster"))
            {
                collision.gameObject.SendMessage("TakeDamage", damage);
            }
        } else if (gameObject.CompareTag("Regular Monster Sword") || gameObject.CompareTag("Tank Monster Club"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.SendMessage("PlayerDamaged", damage);
            }
        } 
    }
}
