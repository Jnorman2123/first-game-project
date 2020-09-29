using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Variables for enemy prefabs, boosts, and player
    public GameObject[] enemyPrefabs;
    public GameObject[] boosts;
    //Variables for random spawn location
    private Vector3 enemySpawnPos;
    private Vector3 boostSpawnPos;
    //Variables for spawn ranges
    private float zRange = 13.5f;
    private float xRange = 24.0f;
    //Number of monsters variables
    private GameObject[] regularMonsters;
    private GameObject[] fastMonsters;
    private GameObject[] tankMonsters;
    private int allMonsters;
    private int waveNumber;

    // Start is called before the first frame update
    void Start()
    {
        //Set waveNumber to 1 intitially
        waveNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Find all monsters and set the number
        regularMonsters = GameObject.FindGameObjectsWithTag("Regular Monster");
        fastMonsters = GameObject.FindGameObjectsWithTag("Fast Monster");
        tankMonsters = GameObject.FindGameObjectsWithTag("Tank Monster");
        allMonsters = regularMonsters.Length + fastMonsters.Length + tankMonsters.Length;
        //If no monsters left stop previous SpawnBoost function, spawn a new wave, and increment the wave number
        if (allMonsters == 0)
        {
            CancelInvoke("SpawnBoost");
            NextWave(waveNumber);
            waveNumber += 1; 
        }
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
    //Function to spawn a wave by number
    private void NextWave(int wave)
    {
       switch (wave)
        {
            case 1:
                Wave(3, 0, 0);
                break;
            case 2:
                Wave(3, 2, 0);
                break;
            case 3:
                Wave(3, 2, 2);
                break;
            case 4:
                Wave(5, 2, 2);
                break;
            case 5:
                Wave(5, 3, 3);
                break;
            default:
                break;
        }
    }
    //Function to spawn the wave of monsters
    private void Wave(int iArg, int nArg, int jArg)
    {
        //Spawn 3 regular monsters
        for (int i = 0; i < iArg; i++)
        {
            spawnMonster(0);
        }
        //Spawn 2 fast monsters
        for (int n = 0; n < nArg; n++)
        {
            spawnMonster(1);
        }
        //Spawn 2 tank monsters
        for (int j = 0; j < jArg; j++)
        {
            spawnMonster(2);
        }
        //Spawn a random boost every 10 seconds after 5 second delay
        InvokeRepeating("SpawnBoost", 5.0f, 10.0f);
    }
    //Function to spawn a random boost
    private void SpawnBoost()
    {
        //Random index to spawn
        int boostIndex = Random.Range(0, boosts.Length);
        //Variables for spawn position
        float yPos = 0.5f;
        boostSpawnPos = new Vector3(Random.Range(xRange, -xRange), yPos, Random.Range(zRange, -zRange));
        //Spawn boost
        Instantiate(boosts[boostIndex], boostSpawnPos, boosts[boostIndex].transform.rotation);
    }
}
