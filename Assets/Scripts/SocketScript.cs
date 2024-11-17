using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketScript : MonoBehaviour
{
    private GameObject animal;

    public void AnimalIn()
    {
        animal = GameObject.FindGameObjectWithTag("Animal");
        animal.GetComponent<PathFinding>().socketSnap = true;
    }

    public void AnimalOut()
    {
        animal = GameObject.FindGameObjectWithTag("Animal");
        animal.GetComponent<PathFinding>().socketSnap = false;
    }
}
