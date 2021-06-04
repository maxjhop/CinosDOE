using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellScript : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject explode;
    public Transform spell;
    private AudioSource explosion;

    public float SpellDamage = 50;
    bool collide = false;

    // Start is called before the first frame update
    void Start()
    {
        explosion = explode.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            if(collide == false)
            {
                create_explosion();
                if (collision.gameObject.tag == "Enemy")
                {
                    Debug.Log("Enemy hit");
                    Enemy enemyhealth = collision.gameObject.transform.GetComponent<Enemy>();
                    //Debug.Log(enemyhealth.)
                    enemyhealth.TakeDamage(SpellDamage);
                }
                if (collision.gameObject.name == "LevelOneBoss")
                {
                    Debug.Log("Boss hit");
                    Enemy boss = collision.gameObject.transform.GetComponent<Enemy>();
                    boss.TakeDamage(SpellDamage);
                }
            }
           
        }
    }
    void create_explosion()
    {
        collide = true;
        //explosion.Play();
        var projectileObj = Instantiate(explode, spell.position, Quaternion.identity) as GameObject;
        Debug.Log("BOOM!");
        Destroy(gameObject);
        Destroy(projectileObj, 5);

    }
}
