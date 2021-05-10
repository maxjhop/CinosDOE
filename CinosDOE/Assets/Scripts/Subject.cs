using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    private List<Observer> observers_ = new List<Observer>();
    private int num_observers = 0;

    public void AddObserver(Observer observer)
    {
        observers_.Add(observer);
        num_observers++;
    }

    public void DeleteObserver(Observer observer)
    {
        observers_.Remove(observer);
        num_observers--;
    }

    protected void notify(GameObject gameObject, int num)
    {
        for (int i = 0; i < num_observers; i++)
        {
            observers_[i].onNotify(gameObject, num);
        }
    }
}

