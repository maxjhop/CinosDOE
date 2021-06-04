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
    private float addedTime = 3f;
    private int totalEnemies;
    private int waveNum = 0;
    public int MAX_ENEMIES;
    public int MAX_WAVES;  // could go up to five
    // might want to change number of enemies per wave
    // could have predetermined constants for each wave size

    // wave intermission info
    private bool waveOneDone = false;
    private bool firstTimeSet = true;
    public float waveIntermissionTime;  // set to 30-60 seconds for final
    private float waveEndTime = 0f;
    public GameObject shop;

    // enemy info
    public GameObject enemy; // the prefab
    private GameObject enemyInstance; // prefab instance

    // enemy2 info
    public GameObject enemy2;
    private GameObject enemy2Instance;

    // boss info
    public GameObject boss;
    private bool alreadySpawned = false;

    private int curEnemies;

    // wave UI text info
    public GameObject waveString;
    private WaveCounter waveCounterScript;
    //public WaveCounter waveCounterScript;

    // Start is called before the first frame update
    void Start()
    {
        waveCounterScript = waveString.GetComponent<WaveCounter>();
        Debug.Log(waveCounterScript);
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
        
        // spawn boss
        if (waveNum == MAX_WAVES && !alreadySpawned)
        {
            GameObject newBoss = Instantiate(boss.gameObject, new Vector3(0, 2, 0), Quaternion.identity) as GameObject;
            alreadySpawned = true;
        }

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
        /*
        if (!waveOneDone)
        {
            Debug.Log(Time.time);
            Debug.Log("starting wave 1!!\n");
            WaveSpawn();
        }
        else {
            float diff = Time.time - waveEndTime;
            //Debug.Log("diff:\n");
            //Debug.Log(diff);
            if (diff >= waveIntermissionTime)
            {
                WaveSpawn();
            }
        }
        */
        WaveSpawn();
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
        if(waveNum == 0)
        {
            GameObject newEnemy = Instantiate(enemy.gameObject, newSpawnLocation + new Vector3(0, 3, 0), Quaternion.identity) as GameObject;

        }
        else
        {
            GameObject newEnemy = Instantiate(enemy2.gameObject, newSpawnLocation + new Vector3(0, 3, 0), Quaternion.identity) as GameObject;
        }
        //Debug.Log("NEW ENEMY SPAWNED");
        totalEnemies += 1;
    }

    void WaveSpawn()
    {
        
        //waveOneDone = true;
        //waveStartTime = Time.time;
        //Debug.Log("Starting time:\n");
        //Debug.Log(waveEndTime);
        
        if (totalEnemies >= MAX_ENEMIES)
        {
            if (curEnemies == 0)
            {
                if (firstTimeSet)
                {
                    shop.SetActive(true);
                    Debug.Log("this is a fukn test");
                    waveEndTime = Time.time;
                    //Debug.Log(waveEndTime);
                    //Debug.Log(Time.time);
                    firstTimeSet = false;
                }
                float diff = Time.time - waveEndTime;
                //Debug.Log(diff);
                //Debug.Log(waveIntermissionTime);
                if (diff >= waveIntermissionTime)
                {
                    shop.SetActive(false);
                    firstTimeSet = true;
                    // waveEndTime = Time.time;
                    //Debug.Log(waveEndTime);
                    totalEnemies = 0;
                    waveNum += 1;
                    /*
                    if(textUI != null)
                    {
                        //Debug.Log(textUI);
                        //textUI.GetComponent<WaveCounter>().UpdateNum();
                        //textUI.UpdateNum();
                    }
                    */
                    waveCounterScript.UpdateNum();
                }
            }
        }
    }
}
