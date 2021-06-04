using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public static TimerScript Instance { get; private set; }

    private float Timer = 0f;
    //private string abilities;


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
        Debug.Log("Instance");
    }

    void Update()
    {
        Timer = Timer + Time.deltaTime;
        int minutes = (int)(Timer / 60);
        int seconds = (int)(Timer % 60);
        Debug.Log("Minutes" + minutes.ToString());
        Debug.Log("Seconds" + seconds.ToString());
    }

    public float getTime()
    {
        
        return Timer;
    }

}