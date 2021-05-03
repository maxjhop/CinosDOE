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
    public int MAX_ENEMIES = 5;

    // enemy info
    public GameObject enemy; // the prefab
    private GameObject enemyInstance; // prefab instance

    // Start is called before the first frame update
    void Start()
    {
        enemyInstance = Instantiate(enemy, new Vector3(1, 2, 1), Quaternion.identity) as GameObject;
        //enemyInstance.transform.Rotate(0f, 180f, 0f);
        // create list of spawn point locations to randomly spawn enemies at 
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
        numberSpawnPoints = spawnPoints.Length;
        spawnPointPositions = new List<Vector3>(); 
        for (int i = 0; i < numberSpawnPoints; i++)
        {
            // spawnPoints[i].transform.position += new Vector3(0, 3, 0);
            spawnPointPositions.Add(spawnPoints[i].transform.position);
        }
        //SpawnEnemy();
        //SpawnEnemy();
        //SpawnEnemy();
        //SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        // numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (Time.time > intervalTime && totalEnemies < MAX_ENEMIES)
        {
            //SpawnEnemy();
            intervalTime += addedTime;
            totalEnemies += 1;
            Debug.Log("Spawning Enemy!");
        }

    }

    // spawns enemy at random spawner
    void SpawnEnemy()
    {
        // spawnPointPositions[Random.Range(1, numberSpawnPoints + 1)] += new Vector3(0, 3, 0)
        //Enemy enemyClone = Instantiate(enemy, new Vector3(1, 2, 1), Quaternion.identity);
        //GameObject newEnemy = enemyClone.gameObject;
        GameObject newEnemy = Instantiate(enemy.gameObject, new Vector3(1, 2, 1), Quaternion.identity) as GameObject;
        Debug.Log(newEnemy);
    }
}
