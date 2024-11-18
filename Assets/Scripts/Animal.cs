/*
 * 
 * Author: Alfred Kang Jing Rui
 * Date: 16/11/2024
 * Purpose: Animal Behaviour and Canvas Script
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents an animal in the game with hunger, thirst, and mood mechanics.
/// </summary>
public class Animal : MonoBehaviour
{
    /// <summary>
    /// UI element representing the hunger level of the animal.
    /// </summary>
    [SerializeField] private Image hungerBar;

    /// <summary>
    /// UI element representing the thirst level of the animal.
    /// </summary>
    [SerializeField] private Image thirstBar;

    /// <summary>
    /// UI element representing the mood level of the animal.
    /// </summary>
    [SerializeField] private Image moodBar;

    /// <summary>
    /// Animator used to control the animal's animations.
    /// </summary>
    [SerializeField] private Animator animator;

    /// <summary>
    /// Reference to the player's camera.
    /// </summary>
    private GameObject playerCam;

    /// <summary>
    /// Canvas object used for displaying the animal's UI elements.
    /// </summary>
    [SerializeField] private GameObject canvasObject;

    /// <summary>
    /// Current hunger level of the animal.
    /// </summary>
    public float hunger = 100f;

    /// <summary>
    /// Current thirst level of the animal.
    /// </summary>
    public float thirst = 100f;

    /// <summary>
    /// Current mood level of the animal.
    /// </summary>
    public float mood = 100f;

    /// <summary>
    /// Initializes the animal's properties and starts the decrease routines.
    /// </summary>
    private void Start()
    {
        // Find the player's main camera by tag.
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        
        // Start coroutines to decrease hunger, thirst, and mood over time.
        StartCoroutine(DecreaseHunger());
        StartCoroutine(DecreaseThirst());
        StartCoroutine(DecreaseMood());
    }

    /// <summary>
    /// Updates the UI canvas to face the player's camera.
    /// </summary>
    private void Update()
    {
        canvasObject.transform.LookAt(playerCam.transform.position);
    }

    /// <summary>
    /// Decreases the animal's hunger level over time. Ends the game when hunger reaches 0.
    /// </summary>
    /// <returns>Coroutine for decreasing hunger.</returns>
    private IEnumerator DecreaseHunger()
    {
        while (hunger > 0)
        {
            if (GameManager.instance.gameStarted && !GameManager.instance.eatingFood)
            {
                hunger -= 3f;
            }
            
            hungerBar.fillAmount = hunger / 100f;
            yield return new WaitForSeconds(1f);
        }

        // Trigger game over when hunger depletes.
        GameManager.instance.GameOver();
    }

    /// <summary>
    /// Decreases the animal's thirst level over time. Ends the game when thirst reaches 0.
    /// </summary>
    /// <returns>Coroutine for decreasing thirst.</returns>
    private IEnumerator DecreaseThirst()
    {
        while (thirst > 0)
        {
            if (GameManager.instance.gameStarted && !GameManager.instance.drinkingWater)
            {
                thirst -= 1f;
            }

            thirstBar.fillAmount = thirst / 100f;
            yield return new WaitForSeconds(1f);
        }
        
        // Trigger game over when thirst depletes.
        GameManager.instance.GameOver();
    }
    
    /// <summary>
    /// Decreases the animal's mood level over time. Ends the game when mood reaches 0.
    /// </summary>
    /// <returns>Coroutine for decreasing mood.</returns>
    private IEnumerator DecreaseMood()
    {
        while (mood > 0)
        {
            if (GameManager.instance.gameStarted && !GameManager.instance.playing)
            {
                mood -= 0.8f;
            }

            moodBar.fillAmount = mood / 100f;
            yield return new WaitForSeconds(1f);
        }
        
        // Trigger game over when mood depletes.
        GameManager.instance.GameOver();
    }
}
