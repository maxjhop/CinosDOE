using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounter : MonoBehaviour
{
    public Text waveNumber;
    private int num = 1;
    //private int duration = 6;
    //public bool testFlag = false;
    public bool beginWave = true;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber.text += num.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // only show text for a few seconds at the start of each wave
        if(beginWave)
        {
            //waveNumber.enabled = false;
            //beginWave = false;
        }
        if (testFlag)
        {
            UpdateNum();
            testFlag = false;
        }
        */
    }

    public void UpdateNum()
    {
        Debug.Log("in UpdateNum()");
        num += 1;
        waveNumber.text = "Wave " + num.ToString();
    }
}
