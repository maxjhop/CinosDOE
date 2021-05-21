using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotation : MonoBehaviour
{
    //public GameObject cloud;
    private GameObject cloud;
    public float speed = 20;
    public Vector3 point = new Vector3(0, 70, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        cloud = this.gameObject;
        //Debug.Log("In cloud script.");
        //Debug.Log(cloud);
    }

    // Update is called once per frame
    void Update()
    {
        // rotate around center (around the future boss)
        transform.RotateAround(new Vector3(0, 90, 0), Vector3.up, speed * Time.deltaTime);
    }
}
