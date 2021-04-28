using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    
    public Camera cam;
    public GameObject spell;
    public Transform firepoint;
    public float projectileSpeed = 30;
    private Vector3 destination;
    public GameObject wand;
    Animator otherAnimator;
    // Start is called before the first frame update
    void Start()
    {
        otherAnimator = wand.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (otherAnimator.GetBool("Fire") == false)
            {
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                    destination = hit.point;
                else
                    destination = ray.GetPoint(1000);

                var projectileObj = Instantiate(spell, firepoint.position, Quaternion.identity) as GameObject;
                projectileObj.GetComponent<Rigidbody>().velocity = (destination - firepoint.position).normalized * projectileSpeed;
            }
        }
        
    }
}
