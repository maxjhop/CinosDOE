using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFloating : MonoBehaviour
{
    private GameObject cloud;
    //private float startingHeight;
    private Vector3 startingPosition;
    private Vector3 endingPosition;
    public float maxShift = 5f;
    private float startingTime;
    private float totalDistance;
    public float bobbingSpeed = .75f;
    // public float increment = .2f;

    // Start is called before the first frame update
    void Start()
    {
        cloud = this.gameObject;
        //startingHeight = this.transform.position.y;
        startingPosition = transform.position;
        endingPosition = new Vector3(startingPosition.x, startingPosition.y + maxShift, startingPosition.z);
        //Debug.Log(startingPosition);
        //Debug.Log(endingPosition);
        startingTime = Time.time;
        totalDistance = Vector3.Distance(startingPosition, endingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // the following code was attempting to make the clouds bob up and down \
        // but it wasn't working and I don't think it's worth it
        /*
        float elapsedDistance = (Time.time - startingTime) * bobbingSpeed;
        float curDistance = elapsedDistance / totalDistance;
        if (curDistance == 1)
        {
            startingTime = Time.time;
            Vector3 temp = startingPosition;
            startingPosition = endingPosition;
            endingPosition = temp;
            totalDistance = Vector3.Distance(startingPosition, endingPosition);

        }
        //float curPos = cloud.transform.position.y;
        //curPos += increment;
        // FIXME
        transform.position = Vector3.Lerp(startingPosition, endingPosition, curDistance);
        //Debug.Log("updating cloud pos");
        
        if (curPos > curPos + maxShift)
        {
            increment *= -1;
        }
        if (curPos < curPos - maxShift)
        {
            increment *= -1;
        }
        */
    }
}
