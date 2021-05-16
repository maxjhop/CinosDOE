using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTracker : MonoBehaviour
{
    public static AbilityTracker Instance { get; private set; }

    private List<string> Abilities = new List<string>();
    //private string abilities;


    private void Awake()
    {
        Instance = this;
        Debug.Log("Instance");
    }

    public void AddAbility(string ability)
    {
        Abilities.Add(ability);
        //abilities = ability;
        //Debug.Log(abilities);
    }

    public bool HasAbility(string ability)
    {
        return Abilities.Contains(ability);
    }

}

