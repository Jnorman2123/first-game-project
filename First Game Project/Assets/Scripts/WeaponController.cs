using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Variables for attack damage
    private float damage;
    // Variables for each character damage
    public float playerDamage = 50.0f;
    private float regularMonsterDamage = 25.0f;
    private float tankMonsterDamage = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.CompareTag("Player Weapon"))
        {
            StartCoroutine("DestroyEnemyWeapon");
        }
    }
    //On collision with monster do damage to monster
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Player Weapon"))
        {
            damage = playerDamage;
            if (collision.gameObject.CompareTag("Regular Monster") || collision.gameObject.CompareTag("Tank Monster") || collision.gameObject.CompareTag("Fast Monster"))
            {
                collision.gameObject.SendMessage("TakeDamage", damage);
            }
        } else if (gameObject.CompareTag("Regular Monster Sword") || gameObject.CompareTag("Tank Monster Club"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                damage = regularMonsterDamage;
                collision.gameObject.SendMessage("PlayerDamaged", damage);
            }
        } else if (gameObject.CompareTag("Tank Monster Club"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                damage = tankMonsterDamage;
                collision.gameObject.SendMessage("PlayerDamaged", damage);
            }
        }
    }

    // Function to destroy the enemy weapon after life span is up
    IEnumerator DestroyEnemyWeapon()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
