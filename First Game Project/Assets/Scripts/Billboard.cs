using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    public GameObject player;

    private Vector3 offset;
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
        offset = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1);
        transform.position = offset;
    }
}
