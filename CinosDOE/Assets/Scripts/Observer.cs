using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public virtual void onNotify(GameObject gameObject, int num) { }
}
