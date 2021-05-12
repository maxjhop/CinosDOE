using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounter : MonoBehaviour
{
    public Text waveNumber;
    private int num =0;
    private float elapsed = 0f;
    public bool showingText = false;
    public int MAX_WAVES = 3;

    // Start is called before the first frame update
    void Start()
    {
        UpdateNum();
    }

    // Update is called once per frame
    void Update()
    {
        if (showingText && (Time.time - elapsed) > 4)
        {
            waveNumber.enabled = false;
        }
        if(num > MAX_WAVES)
        {
            waveNumber.text = "LEVEL COMPLETE!";
            showingText = false;
        }
    }

    public void UpdateNum()
    {
        Debug.Log("in UpdateNum()");
        num += 1;
        waveNumber.enabled = true;
        waveNumber.text = "Wave " + num.ToString();
        elapsed = Time.time;
        showingText = true;
    }
}
