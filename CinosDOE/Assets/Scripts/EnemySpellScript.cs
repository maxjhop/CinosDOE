using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpellScript : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject explode;
    public Transform spell;
    private AudioSource explosion;

    public float SpellDamage = 20;


    private PlayerStats pstats;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        explosion = explode.GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        pstats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            create_explosion();
            pstats.TakeDamage(SpellDamage);
            Debug.Log("Called Take damage");

        }

      
    }
    void create_explosion()
    {
        
        //explosion.Play();
        var projectileObj = Instantiate(explode, spell.position, Quaternion.identity) as GameObject;
        Debug.Log("BOOM!");
        Destroy(gameObject);
        Destroy(projectileObj, 5);

    }
}