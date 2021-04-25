using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellScript : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject explode;
    public Transform spell; 
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
            var projectileObj = Instantiate(explode, spell.position, Quaternion.identity) as GameObject;
            Debug.Log("BOOM!");
            Destroy(gameObject);
        }
    }
}
