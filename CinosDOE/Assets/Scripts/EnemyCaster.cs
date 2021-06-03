using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCaster : MonoBehaviour
{
    public GameObject player;
    private GameObject levelOneBoss;
    private int numEnemies;

    private int experienceDrop = 5;
    public int speed = 5;
    public float EnemyHealth = 100f;
    public float Damage = 50f;
    public float rotationSpeed = 2f;
    public bool isFrozen = false;

    //colton stuff
    public float MovementRange = 3f;
    public float AttackRange = 3f;
    private Vector3 moveDirection;
    public Transform playertrans;
    //NavMeshAgent = agent;

    public Rigidbody rb;

    //for shooting stuff
    public GameObject spell;

    public float projectileSpeed = 30;
    private Vector3 destination;
    public float firerate = 5f;
    private float nextfire = 5f;
    private float nextMove = 5f;
    //private float moveTime = 1f;
    private bool canMove = false;
    //private bool isMoving = false;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playertrans = player.transform;
        rb = GetComponent<Rigidbody>();
        nextMove += 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!isFrozen)
        {
            Vector3 direction = playertrans.position - this.transform.position;
            direction.y = 0;
            //transform.LookAt(playertrans.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                      Quaternion.LookRotation(direction), 0.3f);
            // moveDirection = direction.normalized;

            //transform.LookAt(player.transform);

            float dist = Vector3.Distance(playertrans.position, transform.position);
            if (dist > MovementRange && canMove)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }

            if (Time.time > nextfire)
            {
                nextfire = Time.time + firerate;
                shoot();

            }
        }
        
        if (Time.time > nextMove)
        {
            nextMove = Time.time + nextMove;

            canMove = true;
            isMoving = true;
        }
        if (Time.time > moveTime)
        {
            moveTime= Time.time + moveTime;
            canMove = false;
            isMoving = false;
        }
        */
    }


    public void shoot()
    {

        Vector3 addition;
        addition.x = 0;
        addition.z = 0;
        addition.y = 4;

        var projectileObj = Instantiate(spell, (this.transform.position + (transform.forward * 2) + addition), Quaternion.identity) as GameObject;
        //otherAnimator.SetTrigger("Fire");
        projectileObj.GetComponent<Rigidbody>().velocity = (transform.forward).normalized * projectileSpeed;
        Destroy(projectileObj, 8);
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

        Debug.Log("DESTROYING ENEMY!");

        Destroy(gameObject);
        ExpManager.Instance.AddExperience(experienceDrop);

        //ExpManager.onNotify(player, 1);
        //AddExperience(experienceDrop);


    }


}
