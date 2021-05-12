using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private GameObject levelOneBoss;
    private int numEnemies;

    private int experienceDrop = 5;
    public int speed = 5;
    public float EnemyHealth = 100f;
    public float Damage = 50f;
    public float rotationSpeed = 2f;

    //colton stuff
    public float MovementRange = 3f;
    public float AttackRange =  3f;
    private Vector3 moveDirection;
    public Transform playertrans;
    //NavMeshAgent = agent;

    public Rigidbody rb;

    
    void Awake()
    {
        
     
        // immediately disable boss

        if (this.name == "LevelOneBoss")
        {

            levelOneBoss = GameObject.Find("LevelOneBoss");
            //Debug.Log("Inside awake:", levelOneBoss);


        }
       
        if (this.name == "LevelOneBoss" && levelOneBoss != null)
        {
            // print("Setting boss activity to false");
            EnemyHealth = 200f;
            Damage = 75f;
            levelOneBoss.SetActive(false);
            //Debug.Log("Boss is not null");
        }
       
    }


    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.FindWithTag("Player");
        playertrans = player.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // check for number of enemies (boss spawns when all enemies are dead)
        numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //moveDirection = Vector3.zero;

        
        Vector3 direction = playertrans.position - this.transform.position;
        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                    Quaternion.LookRotation(direction), 0.3f);
        //moveDirection = direction.normalized;

        //transform.LookAt(player.transform);

        float dist = Vector3.Distance(playertrans.position, transform.position);
        if (dist > MovementRange)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (levelOneBoss == null)
        {
            //Debug.Log("BOSS WENT NULL");
            //Debug.Log(this.name);
        }

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
            Debug.Log("Last enemy has been killed");
            //Debug.Log(levelOneBoss);
            
            if (levelOneBoss != null) {
                //Debug.Log("Setting Boss to active");
                levelOneBoss.SetActive(true);
            }
            
            //levelOneBoss.SetActive(true);
        }
        Debug.Log("DESTROYING ENEMY!");
        
        Destroy(gameObject);
        ExpManager.Instance.AddExperience(experienceDrop);

        //ExpManager.onNotify(player, 1);
        //AddExperience(experienceDrop);


    }


}
