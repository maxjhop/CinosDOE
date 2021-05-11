using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance { get; private set; }

    private float experience = 0;
    public Text EXP;

    private void Awake()
    {
        Instance = this;
    }

    public void AddExperience(int exp)
    {
        experience += exp;
        EXP.text = "Experience: " + experience.ToString();
    }

    public void SubExperience(int exp)
    {
        experience -= exp;
        EXP.text = "Experience: " + experience.ToString();
    }

    public float GetExperience()
    {
        return experience;
    }



}
