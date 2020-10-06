using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    // Restart button variable and spawn manager
    private Button restartButton;
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        // Set restartButton and spawnManager
        restartButton = GetComponent<Button>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        // Add click listener to restartButton
        restartButton.onClick.AddListener(RestartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RestartGame()
    {
        Debug.Log("restart game");
        spawnManager.RestartGame();
    }
}
