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
        //Set spawn pos for enemies to random location
        spawnPos = new Vector3(Random.Range(xRange, -xRange), 0.5f, Random.Range(zRange, -zRange));

        Instantiate(enemyPrefabs[0], spawnPos, enemyPrefabs[0].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
