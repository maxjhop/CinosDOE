using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // spawner info
    private List<Vector3> spawnPointPositions;
    private GameObject[] spawnPoints;
    private int numberSpawnPoints;

    // enemy wave info
    private float intervalTime = 0.0f;
    private float addedTime = 1f;
    private int totalEnemies;
    private int waveNum = 0;
    public int MAX_ENEMIES = 5;
    public int MAX_WAVES = 3;  // could go up to five
    // might want to change number of enemies per wave
    // could have predetermined constants for each wave size

    // enemy info
    public GameObject enemy; // the prefab
    private GameObject enemyInstance; // prefab instance
    private int curEnemies;

    // Start is called before the first frame update
    void Start()
    {
        //enemyInstance = Instantiate(enemy, new Vector3(1, 2, 1), Quaternion.identity) as GameObject;
        //enemyInstance.transform.Rotate(0f, 180f, 0f);
        // create list of spawn point locations to randomly spawn enemies at 
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
        numberSpawnPoints = spawnPoints.Length;
        //Debug.Log("NUM SPAWN POINTS");
        //Debug.Log(numberSpawnPoints);
        spawnPointPositions = new List<Vector3>();
        for (int i = 0; i < numberSpawnPoints; i++)
        {
            // spawnPoints[i].transform.position += new Vector3(0, 3, 0);
            spawnPointPositions.Add(spawnPoints[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // <begin pseudocode>
        // for each wave:
        // ... display "Wave n" 
        // ... gradually spawn all enemies
        // ... while enemies left > 1:
        // ... ... wait for Cinos to destroy all enemies
        // ... ... offer heals
        // ... ... introduce other obstacles
        // ... open shop for upgrades
        // spawn boss
        // <end pseudocode>
        curEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (Time.time > intervalTime && totalEnemies < MAX_ENEMIES && waveNum < MAX_WAVES)
        {
            Debug.Log(waveNum);
            //Debug.Log(Time.time);
            //Debug.Log(intervalTime);
            SpawnEnemy();
            intervalTime += addedTime;
            // totalEnemies += 1;
            //Debug.Log("Spawning Enemy!");
        }
        if (totalEnemies >= MAX_ENEMIES)
        {
            if (curEnemies == 0)
            {
                totalEnemies = 0;
                waveNum += 1;
            }
        }
    }

    // spawns enemy at random spawner
    void SpawnEnemy()
    {
        int randLoc = Random.Range(0, numberSpawnPoints);
        //Debug.Log("RAND LOC");
        //Debug.Log(randLoc);
        Vector3 newSpawnLocation = spawnPointPositions[randLoc]; // += new Vector3(0, 3, 0);
        //Enemy enemyClone = Instantiate(enemy, new Vector3(1, 2, 1), Quaternion.identity);
        //GameObject newEnemy = enemyClone.gameObject;
        GameObject newEnemy = Instantiate(enemy.gameObject, newSpawnLocation, Quaternion.identity) as GameObject;
        Debug.Log("NEW ENEMY SPAWNED");
        totalEnemies += 1;
    }
}
