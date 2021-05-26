using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudParticleSystem : MonoBehaviour
{
    public ParticleSystem clouds; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Checking cloud particle system...");
        Debug.Log(clouds);
        Debug.Log(clouds.isEmitting);
        if (!clouds.isPlaying)
        {
            clouds.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
