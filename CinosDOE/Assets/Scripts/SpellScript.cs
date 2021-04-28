using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellScript : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject explode;
    public Transform spell;

    public float SpellDamage = 50;
    bool collide = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
                    Debug.Log("Is enemy");
                    Enemy enemyhealth = collision.gameObject.transform.GetComponent<Enemy>();
                    enemyhealth.TakeDamage(SpellDamage);
                }
            }
           
        }
    }
    void create_explosion()
    {
        collide = true;
        var projectileObj = Instantiate(explode, spell.position, Quaternion.identity) as GameObject;
        Debug.Log("BOOM!");
        Destroy(gameObject);

    }
}
