using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Variables for enemy prefabs
    public GameObject[] enemyPrefabs;
    //Variable for random spawn location
    private Vector3 spawnPos;
    //Variables for spawn ranges
    private float zRange = 13.5f;
    private float xRange = 24.0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnMonster(0);
        spawnMonster(1);
        spawnMonster(2);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Function to spawn a monster and a random location
    private void spawnMonster(int monsterIndex)
    {
        //Set spawn pos for enemies to random location
        spawnPos = new Vector3(Random.Range(xRange, -xRange), 0.5f, Random.Range(zRange, -zRange));
        //Spawn enemy of given index
        Instantiate(enemyPrefabs[monsterIndex], spawnPos, enemyPrefabs[monsterIndex].transform.rotation);
    }
}
