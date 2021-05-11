using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounter : MonoBehaviour
{
    public Text waveNumber;
    private int num = 0;
    private int duration = 4;
    public bool testFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        waveNumber.text += num.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // only show text for a few seconds at the start of each wave
        if(Time.time > duration)
        {
            waveNumber.enabled = false;
        }

        if (testFlag)
        {
            UpdateNum();
            testFlag = false;
        }
    }

    void UpdateNum()
    {
        num += 1;
        waveNumber.text = "Wave " + num.ToString();
    }
}
