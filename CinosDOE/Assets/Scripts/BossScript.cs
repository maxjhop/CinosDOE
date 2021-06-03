using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossScript : MonoBehaviour
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
    public float AttackRange = 3f;
    public float AttackDamage = 10f;
    private Vector3 moveDirection;
    public Transform playertrans;
    //NavMeshAgent = agent;

    public Rigidbody rb;


    //dealing damage cooldown
    bool CanAttack = true;


    //for shooting stuff
    public GameObject spell_one;
    public GameObject spell_two;

    public float projectileSpeed = .5f;
    public float firerate = 1f;
    private float nextfire = 1f;
    private int shoot_color = 0;




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
        else
        {
            if (CanAttack == true)
            {
                StartCoroutine(DealDamage(AttackDamage));
                Debug.Log("DealthDamage");

            }

        }

        if (Time.time > nextfire)
        {
            /* uncommit later
            shoot_color = Random.Range(0, 2);
            if (shoot_color == 0)
            {
             
                shoot_Red();
            }
            else
            {
                shoot_Black();
            }
            */
            shoot_Red();
            nextfire = Time.time + firerate;
        }


    }

    public void shoot_Red()
    {
        //raise above the ground 
        Vector3 addition;
        addition.x = 0;
        addition.z = 0;
        addition.y = 4;


        var projectileObj_N = Instantiate(spell_one, (this.transform.position + (transform.forward * 2) + addition), Quaternion.identity) as GameObject;
        projectileObj_N.GetComponent<Rigidbody>().velocity = (transform.forward).normalized * projectileSpeed;

        var projectileObj_E = Instantiate(spell_one, (this.transform.position + (transform.right * 2) + addition), Quaternion.identity) as GameObject;
        projectileObj_E.GetComponent<Rigidbody>().velocity = (transform.right).normalized * projectileSpeed;

        var projectileObj_W = Instantiate(spell_one, (this.transform.position + (transform.forward * 2) + addition), Quaternion.identity) as GameObject;
        projectileObj_W.GetComponent<Rigidbody>().velocity = (-transform.right).normalized * projectileSpeed;
        
    }

    public void shoot_Black()
    {

        Vector3 addition;
        addition.x = 0;
        addition.z = 0;
        addition.y = 4;

        var projectileObj = Instantiate(spell_two, (this.transform.position + (transform.forward * 2) + addition), Quaternion.identity) as GameObject;
        //otherAnimator.SetTrigger("Fire");
        projectileObj.GetComponent<Rigidbody>().velocity = (transform.forward).normalized * projectileSpeed;
    }




    IEnumerator DealDamage(float damage)
    {
        CanAttack = false;
        player.GetComponent<PlayerStats>().TakeDamage(damage);
        yield return new WaitForSeconds(5.0f);
        CanAttack = true;
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

            if (levelOneBoss != null)
            {
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

