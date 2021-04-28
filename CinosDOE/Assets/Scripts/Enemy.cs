using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public int speed = 5;

    private GameObject levelOneBoss;
    private int numEnemies; 

    public float EnemyHealth = 100f;
    public float Damage = 50;

    void Awake()
    {
        // immediately disable boss
        levelOneBoss = GameObject.Find("LevelOneBoss");
        if (this.name == "LevelOneBoss" && levelOneBoss != null)
        {
            print("Setting boss activity to false");
            EnemyHealth = 200f;
            Damage = 75;
            levelOneBoss.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // check for number of enemies (boss spawns when all enemies are dead)
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
   
        transform.LookAt(player.transform);
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        EnemyHealth -= damage;
        Debug.Log("took damage");
        if (EnemyHealth <= 0)
        {
            EnemyDead();
        }
    }

    void EnemyDead()
    {
        // if last enemy is about to die, spawn boss
        if (numEnemies == 1)
        {
            levelOneBoss.SetActive(true);
        }
        
        Destroy(gameObject);
    }
}
