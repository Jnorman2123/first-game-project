using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Variables for enemy prefabs, boosts, and player
    public GameObject[] enemyPrefabs;
    public GameObject[] boosts;
    private GameObject player;
    //Variables for random spawn location
    private Vector3 enemySpawnPos;
    private Vector3 boostSpawnPos;
    //Variables for spawn ranges
    private float zRange = 13.5f;
    private float xRange = 24.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        WaveOne();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 WeaponPos = player.transform.position + new Vector3(0, 0, 1.5f);
        //if (Input.GetKeyDown(KeyCode.Space))
        //{ 
           //Debug.Log("shoot weapon");
            //Instantiate(weapon, WeaponPos, weapon.transform.rotation);
        //}
    }
    //Function to spawn a monster and a random location
    private void spawnMonster(int monsterIndex)
    {
        //Set y spawn pos based on monster size 
        float yPos = 0;
        if (monsterIndex == 0)
        {
            yPos = 0.5f;
        } else if (monsterIndex == 1)
        {
            yPos = 0.375f;
        } else if (monsterIndex == 2)
        {
            yPos = 1.0f;
        }
        //Set spawn pos for enemies to random location
        enemySpawnPos = new Vector3(Random.Range(xRange, -xRange), yPos, Random.Range(zRange, -zRange));
        //Spawn enemy of given index
        Instantiate(enemyPrefabs[monsterIndex], enemySpawnPos, enemyPrefabs[monsterIndex].transform.rotation);
    }

    private void WaveOne()
    {
        for (int i = 0; i < 5; i++) {
            spawnMonster(0);
        }
        InvokeRepeating("SpawnBoost", 5.0f, 10.0f);
    }

    private void SpawnBoost()
    {
        int boostIndex = Random.Range(0, boosts.Length);
        float yPos = 0.5f;
        boostSpawnPos = new Vector3(Random.Range(xRange, -xRange), yPos, Random.Range(zRange, -zRange));
        Instantiate(boosts[boostIndex], boostSpawnPos, boosts[boostIndex].transform.rotation);
    }
}
