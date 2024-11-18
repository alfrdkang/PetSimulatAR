/*
 * 
 * Author: Alfred Kang Jing Rui
 * Date: 18/11/2024
 * Purpose: Options Menu Script
 * 
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the options menu for the game, including settings for audio, graphics, and background music.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    /// <summary>
    /// Slider UI component for adjusting audio volume.
    /// </summary>
    public Slider audioSlider;

    /// <summary>
    /// Dropdown UI component for selecting graphics quality.
    /// </summary>
    public TMP_Dropdown graphicsDropdown;

    /// <summary>
    /// Toggle UI component for enabling or disabling background music.
    /// </summary>
    public Toggle backgroundMusicToggle;

    /// <summary>
    /// Audio source for the background music.
    /// </summary>
    public AudioSource backgroundMusic;

    /// <summary>
    /// Initializes the menu by loading saved settings and setting up event listeners for UI components.
    /// </summary>
    private void Start()
    {
        LoadSettings();

        // Set up event listeners for UI changes.
        audioSlider.onValueChanged.AddListener(SetAudioVolume);
        graphicsDropdown.onValueChanged.AddListener(SetGraphicsQuality);
        backgroundMusicToggle.onValueChanged.AddListener(SetBackgroundMusic);
    }

    /// <summary>
    /// Sets the audio volume based on the slider value and saves the setting.
    /// </summary>
    /// <param name="volume">The new audio volume (range: 0.0 to 1.0).</param>
    public void SetAudioVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("AudioVolume", volume);
    }

    /// <summary>
    /// Sets the graphics quality based on the dropdown selection and saves the setting.
    /// </summary>
    /// <param name="qualityIndex">The index of the selected graphics quality level.</param>
    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
    }

    /// <summary>
    /// Enables or disables background music based on the toggle value and saves the setting.
    /// </summary>
    /// <param name="isEnabled">True to enable background music; false to disable it.</param>
    public void SetBackgroundMusic(bool isEnabled)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.mute = !isEnabled;
        }

        if (isEnabled)
        {
            PlayerPrefs.SetInt("BackgroundMusic", 1);
        }
        else
        {
            PlayerPrefs.SetInt("BackgroundMusic", 0);
        }
    }

    /// <summary>
    /// Loads the saved settings for audio volume, graphics quality, and background music.
    /// </summary>
    private void LoadSettings()
    {
        // Load and apply saved audio volume.
        if (PlayerPrefs.HasKey("AudioVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("AudioVolume");
            audioSlider.value = savedVolume;
            AudioListener.volume = savedVolume;
        }

        // Load and apply saved graphics quality.
        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            int savedQuality = PlayerPrefs.GetInt("GraphicsQuality");
            graphicsDropdown.value = savedQuality;
            QualitySettings.SetQualityLevel(savedQuality);
        }

        // Load and apply saved background music setting.
        if (PlayerPrefs.HasKey("BackgroundMusic"))
        {
            bool isMusicEnabled = PlayerPrefs.GetInt("BackgroundMusic") == 1;
            backgroundMusicToggle.isOn = isMusicEnabled;
            if (backgroundMusic != null)
            {
                backgroundMusic.mute = !isMusicEnabled;
            }
        }
    }

    /// <summary>
    /// Resets all settings to their default values and updates the UI components accordingly.
    /// </summary>
    public void ResetToDefaults()
    {
        // Reset settings to default values.
        SetAudioVolume(1f);           // Default volume: 100%.
        SetGraphicsQuality(2);        // Default graphics quality: high.
        SetBackgroundMusic(true);     // Default: background music enabled.

        // Update UI components to reflect default values.
        audioSlider.value = 1f;
        graphicsDropdown.value = 2;
        backgroundMusicToggle.isOn = true;
    }
}
