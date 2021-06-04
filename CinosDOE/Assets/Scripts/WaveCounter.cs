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
    public GameObject winscreen;
    public GameObject gameplayui;
    public Text endText;
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
            if ((SceneManager.GetActiveScene().name == "LevelThree"))
            {
                float timer = TimerScript.Instance.getTime();
                int minutes = (int)(timer / 60);
                int seconds = (int)(timer % 60);
                if (seconds < 10)
                    endText.text = "You win! Congratulations! \n Time: " + minutes.ToString() + ":0" + seconds.ToString();
                else
                    endText.text = "You win! Congratulations! \n Time: " + minutes.ToString() + ":" + seconds.ToString();

                winscreen.SetActive(true);
                gameplayui.SetActive(false);
            }
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
