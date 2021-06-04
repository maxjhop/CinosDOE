using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WaveCounter : MonoBehaviour
{
    public Text waveNumber;
    private int num =0;
    private float elapsed = 0f;
    public bool showingText = false;
    public GameObject shop;
    public int MAX_WAVES = 3;
    
    // next level pad info
    //public bool levelComplete = false;
    public GameObject nextLevelPad;

    // Start is called before the first frame update
    void Start()
    {
        if (!(SceneManager.GetActiveScene().name == "TutorialScene"))
        {
            nextLevelPad.SetActive(false);
        }
        UpdateNum();
    }

    // Update is called once per frame
    void Update()
    {
        if (showingText && (Time.time - elapsed) > 4)
        {
            waveNumber.enabled = false;
            showingText = false;
        }
        if(!showingText && (num > MAX_WAVES))
        {
            waveNumber.text = "LEVEL COMPLETE!\n Shop Available!";
            elapsed = Time.time + 4;
            shop.SetActive(true);
            showingText = true;
            nextLevelPad.SetActive(true);
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
        if(num > MAX_WAVES)
        {
            showingText = false;
        }
    }
}
