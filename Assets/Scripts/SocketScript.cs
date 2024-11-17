using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SocketScript : MonoBehaviour
{
    private GameObject animal;

    public bool socketConnected = false;
    private bool canStatus = true;

    public string itemName;

    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private AudioClip animalEatClip;
    [SerializeField] private AudioClip animalDrinkClip;
    [SerializeField] private AudioClip animalPlayClip;

    private void Start()
    {
        audioSrc = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    public void AnimalIn()
    {
        socketConnected = true;
        animal = GameObject.FindGameObjectWithTag("Animal");
        animal.GetComponent<PathFinding>().socketSnap = true;
    }

    public void AnimalOut()
    {
        socketConnected = false;
        animal = GameObject.FindGameObjectWithTag("Animal");
        animal.GetComponent<PathFinding>().socketSnap = false;
    }

    private void Update()
    {
        if (socketConnected && canStatus)
        {
            if (itemName == "PetFood")
            {
                Debug.Log("Eating! Hunger +10");
                
                audioSrc.PlayOneShot(animalEatClip);
                
                GameManager.instance.eatingFood = true;
                animal.GetComponent<Animal>().hunger += 10;
                
                StartCoroutine(Delay());
                
            } else if (itemName == "WaterBowl")
            {
                Debug.Log("Drinking! Thirst +10");
                
                audioSrc.PlayOneShot(animalDrinkClip);
                
                GameManager.instance.drinkingWater = true;
                animal.GetComponent<Animal>().thirst += 10;
                
                StartCoroutine(Delay());
                
            } else if (itemName == "TennisBall")
            {
                Debug.Log("Playing! Mood +10");
                
                audioSrc.PlayOneShot(animalPlayClip);
                
                GameManager.instance.playing = true;
                animal.GetComponent<Animal>().mood += 10;
                
                StartCoroutine(Delay());
                
            }
        }
    }

    private IEnumerator Delay()
    {
        canStatus = false;
        yield return new WaitForSeconds(1);
        canStatus = true;
    }
}
