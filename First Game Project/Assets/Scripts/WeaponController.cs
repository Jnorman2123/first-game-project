using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Variable for attack damage
    public float damage = 50;
    // Variable for sword animation
    public Animation swordAnim;
    // Start is called before the first frame update
    void Start()
    {
        swordAnim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //On collision with monster do damage to monster
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Regular Monster") || other.gameObject.CompareTag("Tank Monster") || other.gameObject.CompareTag("Fast Monster"))
        {
            other.gameObject.SendMessage("TakeDamage", damage);
        }
    }
}
