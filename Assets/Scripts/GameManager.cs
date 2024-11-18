/*
 * 
 * Author: Alfred Kang Jing Rui
 * Date: 16/11/2024
 * Purpose: Game State and Management Script
 * 
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the GameManager.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Panel that appears when the game is over.
    /// </summary>
    [SerializeField] private GameObject gameOverPanel;

    /// <summary>
    /// Panel that appears when the player wins the game.
    /// </summary>
    [SerializeField] private GameObject gameWinPanel;

    /// <summary>
    /// Flag indicating whether the game has started.
    /// </summary>
    public bool gameStarted = false;

    /// <summary>
    /// Flag indicating whether the player is currently eating food.
    /// </summary>
    public bool eatingFood = false;

    /// <summary>
    /// Flag indicating whether the player is currently drinking water.
    /// </summary>
    public bool drinkingWater = false;

    /// <summary>
    /// Flag indicating whether the player is actively playing.
    /// </summary>
    public bool playing = false;

    /// <summary>
    /// The current time in the game.
    /// </summary>
    public int time;

    /// <summary>
    /// The TextMeshPro object used to display the time in the game.
    /// </summary>
    public TextMeshProUGUI timeText;

    /// <summary>
    /// Initializes the GameManager singleton and ensures it persists across scenes.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensures this object persists across scene loads.
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroys duplicate instances.
        }
    }

    /// <summary>
    /// Starts the game and begins the game timer.
    /// </summary>
    public void StartGame()
    {
        gameStarted = true;
        StartCoroutine(StartGameTimer()); // Starts the game timer.
    }

    /// <summary>
    /// Ends the game and shows the game over panel.
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0; // Pauses the game.
        gameOverPanel.SetActive(true); // Activates the game over panel.
    }

    /// <summary>
    /// Ends the game and shows the game win panel.
    /// </summary>
    public void GameWin()
    {
        Time.timeScale = 0; // Pauses the game.
        gameWinPanel.SetActive(true); // Activates the game win panel.
    }

    /// <summary>
    /// Coroutine that counts down the game time and displays it on the screen.
    /// Once time reaches 0, the game is won.
    /// </summary>
    /// <returns>IEnumerator for the coroutine.</returns>
    private IEnumerator StartGameTimer()
    {
        // Loops as long as the time is greater than 0
        while (time > 0)
        {
            timeText.text = time.ToString(); // Updates the time display
            yield return new WaitForSeconds(1f); // Waits for 1 second
            time--; // Decreases the time
        }
        GameWin(); // Calls GameWin once time is up
    }
}
