using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance { get; private set; }

    private float experience = 400;
    public Text EXP;

    private void Awake()
    {
        Instance = this;
        EXP.text = "Experience: " + experience.ToString();
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
