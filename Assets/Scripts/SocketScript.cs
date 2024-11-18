/*
 * 
 * Author: Alfred Kang Jing Rui
 * Date: 16/11/2024
 * Purpose: Socket behaviour and functions script
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Handles the interaction between an animal and a socket in the game,
/// including feeding, hydrating, and playing with the animal.
/// </summary>
public class SocketScript : MonoBehaviour
{
    /// <summary>
    /// Reference to the current animal interacting with the socket.
    /// </summary>
    private Animal animal;

    /// <summary>
    /// Indicates whether the socket is currently connected to an animal.
    /// </summary>
    public bool socketConnected = false;

    /// <summary>
    /// Controls whether an action can be performed (e.g., eating, drinking, playing).
    /// </summary>
    private bool canStatus = true;

    /// <summary>
    /// The name of the item associated with the socket (e.g., "PetFood", "WaterBowl").
    /// </summary>
    public string itemName;

    /// <summary>
    /// Audio source for playing sound effects.
    /// </summary>
    [SerializeField] private AudioSource audioSrc;

    /// <summary>
    /// Audio clip for the animal eating sound effect.
    /// </summary>
    [SerializeField] private AudioClip animalEatClip;

    /// <summary>
    /// Audio clip for the animal drinking sound effect.
    /// </summary>
    [SerializeField] private AudioClip animalDrinkClip;

    /// <summary>
    /// Audio clip for the animal playing sound effect.
    /// </summary>
    [SerializeField] private AudioClip animalPlayClip;

    /// <summary>
    /// Initializes the audio source by finding the object tagged as "SFX".
    /// </summary>
    private void Start()
    {
        audioSrc = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
    }

    /// <summary>
    /// Called when an animal interacts with the socket, establishing a connection.
    /// </summary>
    public void AnimalIn()
    {
        Debug.Log("Animal In!");
        socketConnected = true;
        animal = GameObject.FindGameObjectWithTag("Animal").GetComponent<Animal>();
        animal.GetComponent<PathFinding>().socketSnap = true;
        canStatus = true;
    }

    /// <summary>
    /// Called when the animal leaves the socket, disconnecting it and resetting states.
    /// </summary>
    public void AnimalOut()
    {
        Debug.Log("Animal Out!");
        socketConnected = false;
        animal.GetComponent<PathFinding>().socketSnap = false;
        GameManager.instance.eatingFood = false;
        GameManager.instance.drinkingWater = false;
        GameManager.instance.playing = false;
    }

    /// <summary>
    /// Continuously checks and handles actions (eating, drinking, playing) when the socket is connected.
    /// </summary>
    private void Update()
    {
        if (socketConnected && canStatus)
        {
            if (itemName == "PetFood")
            {
                Debug.Log("Eating! Hunger +10");
                audioSrc.PlayOneShot(animalEatClip);
                GameManager.instance.eatingFood = true;
                animal.hunger += 10;
                if (animal.hunger > 100) animal.hunger = 100;
                StartCoroutine(Delay());
            }
            else if (itemName == "WaterBowl")
            {
                Debug.Log("Drinking! Thirst +10");
                audioSrc.PlayOneShot(animalDrinkClip);
                GameManager.instance.drinkingWater = true;
                animal.thirst += 10;
                if (animal.thirst > 100) animal.thirst = 100;
                StartCoroutine(Delay());
            }
            else if (itemName == "TennisBall")
            {
                Debug.Log("Playing! Mood +10");
                audioSrc.PlayOneShot(animalPlayClip);
                GameManager.instance.playing = true;
                animal.mood += 10;
                if (animal.mood > 100) animal.mood = 100;
                StartCoroutine(Delay());
            }
        }
    }

    /// <summary>
    /// Prevents repeated actions for a short delay, allowing only one action at a time.
    /// </summary>
    /// <returns>A coroutine that delays further actions for 1 second.</returns>
    private IEnumerator Delay()
    {
        canStatus = false;
        yield return new WaitForSeconds(1);
        canStatus = true;
    }
}
