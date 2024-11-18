/*
 * 
 * Author: Alfred Kang Jing Rui
 * Date: 17/11/2024
 * Purpose: Menu Functions and Behaviour Script
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Indicates whether the game is currently paused
    public bool isPaused = false;

    // The pause menu GameObject that is activated/deactivated when paused/unpaused
    [SerializeField] private GameObject pauseMenu;

    /// <summary>
    /// Toggles the pause state of the game.
    /// Pauses the game by setting time scale to 0 and showing the pause menu,
    /// or resumes the game by setting time scale to 1 and hiding the pause menu.
    /// </summary>
    public void Pause()
    {
        if (isPaused)
        {
            // If the game is paused, unpause it by setting the time scale back to normal
            Time.timeScale = 1;
            isPaused = false;
            pauseMenu.SetActive(false); // Hide the pause menu
        }
        else
        {
            // If the game is not paused, pause it by setting the time scale to 0
            Time.timeScale = 0;
            isPaused = true;
            pauseMenu.SetActive(true); // Show the pause menu
        }
    }

    /// <summary>
    /// Restarts the current scene.
    /// This will reload the active scene, effectively restarting the game.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
    
    /// <summary>
    /// Quits the application.
    /// This will close the game when called.
    /// </summary>
    public void Quit()
    {
        Application.Quit(); // Quit the application
    }

}