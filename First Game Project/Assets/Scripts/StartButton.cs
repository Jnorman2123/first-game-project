using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    // Variable for start button and game manager
    private Button startButton;
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        // Set startButton and gameManager
        startButton = GetComponent<Button>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        // Add listener for click to button
        startButton.onClick.AddListener(GameStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Function to call the spawn manager StartGame method
    void GameStart()
    {
        spawnManager.StartGame();
    }
}
