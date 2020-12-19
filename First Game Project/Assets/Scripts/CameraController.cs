using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    // Player game object variable
    public GameObject player;
    // Camera offset
    private Vector3 offset; 
    void Start()
    {
        // Set value of offset
        offset = new Vector3(0, player.transform.position.y + 2, player.transform.position.z - 3);
    }

    // Update is called once per frame
    void Update()
    {
        // Set camera position to just behind the player
        transform.position = player.transform.position + offset;
        // Set camera rotation equal to player rotation
        transform.rotation = player.transform.rotation;
    }
}
