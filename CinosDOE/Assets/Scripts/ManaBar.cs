using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;

    public void SetMana(float mana)
    {
        slider.value = mana;
    }

    public void SetMax(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }
}
