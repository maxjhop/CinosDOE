using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFloating : MonoBehaviour
{
    private GameObject cloud;
    private float startingHeight;
    public float maxShift = 5f;
    public float increment = .2f;

    // Start is called before the first frame update
    void Start()
    {
        cloud = this.gameObject;
        startingHeight = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float curPos = cloud.transform.position.y;
        curPos += increment;
        // FIXME
        // transform.position = Vector3.Lerp(transform.position, )
        if (curPos < curPos + maxShift)
        {
            increment *= -1;
        }
        if (curPos < curPos - maxShift)
        {
            increment *= -1;
        }
    }
}
