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
            if (collide == false)
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    Enemy enemy = collision.gameObject.transform.GetComponent<Enemy>();
                    EnemyCaster enemyCaster = collision.gameObject.transform.GetComponent<EnemyCaster>();
                    //Debug.Log(enemyhealth.)
                    enemy.TakeDamage(SpellDamage);
                    if (this.tag == "Freeze")
                    {
                        enemy.isFrozen = true;

                        if (enemyCaster != null)
                            enemyCaster.isFrozen = true;
                        var ice = enemy.transform.GetChild(2).gameObject;
                        ice.SetActive(true);
                        this.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                        var projectileObj = Instantiate(explode, spell.position, Quaternion.identity) as GameObject;
                        Destroy(projectileObj, 5);
                        this.transform.GetChild(0).gameObject.SetActive(false);
                        this.transform.GetChild(1).gameObject.SetActive(false);
                        this.GetComponent<SphereCollider>().enabled = false;
                        this.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                        StartCoroutine(Freeze(enemy, ice, enemyCaster));
                    }
                    else
                    {
                        Debug.Log("First call");
                        create_explosion();
                    }
                    Debug.Log("Enemy hit");

                }

                else if (collision.gameObject.name == "Boss")
                {
                    Debug.Log("Second Call");
                    create_explosion();
                    Debug.Log("Boss hit");
                    BossScript boss = collision.gameObject.transform.GetComponent<BossScript>();
                    boss.TakeDamage(SpellDamage);
                }

                else
                {
                    Debug.Log("Third call");
                    create_explosion();
                }

            }

        }
    }

    IEnumerator Freeze(Enemy enemy, GameObject ice, EnemyCaster enemyCaster)
    {
        Debug.Log("INSIDE FREEZE");
        yield return new WaitForSeconds(3.0f);
        Debug.Log("AFTER WAIT");
        ice.SetActive(false);
        enemy.isFrozen = false;
        if (enemyCaster != null)
        {
            enemyCaster.isFrozen = false;
        }
        Destroy(gameObject);

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
