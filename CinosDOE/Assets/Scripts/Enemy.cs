using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public int speed = 5;
    public GameObject levelOneBoss;
    public float timer;

    public float EnemyHealth = 100f;

    public float Damage = 50;

    void Awake()
    {
        levelOneBoss = GameObject.Find("LevelOneBoss");
        if (levelOneBoss != null)
        {
            print("called in awake");
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
        // nt(timer);
        // makes mini-boss appear after 10 seconds
        /*
        if (timer > 5.0f && levelOneBoss.activeSelf == false)
        {
            print("ACTIVATING BOSS");
            levelOneBoss.SetActive(true);
        }
        timer += Time.deltaTime;
        */

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
        Destroy(gameObject);
    }
}
