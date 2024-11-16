using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image thirstBar;
    [SerializeField] private Image moodBar;
    
    [SerializeField] private Animator animator;
    
    private GameObject playerCam;
    [SerializeField] private GameObject canvasObject;

    public float hunger = 100f;
    public float thirst = 100f;
    public float mood = 100f;

    private void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        
        StartCoroutine(DecreaseHunger());
        StartCoroutine(DecreaseThirst());
        StartCoroutine(DecreaseMood());
    }

    private void Update()
    {
        canvasObject.transform.LookAt(playerCam.transform.position);
    }

    private IEnumerator DecreaseHunger()
    {
        while (hunger > 0)
        {
            if (GameManager.instance.gameStarted)
            {
                hunger -= 1f;
            }
            
            hungerBar.fillAmount = hunger/100f;
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator DecreaseThirst()
    {
        while (thirst > 0)
        {
            if (GameManager.instance.gameStarted)
            {
                thirst -= 0.5f;
            }

            thirstBar.fillAmount = thirst/100f;
            yield return new WaitForSeconds(1f);
        }
    }
    
    private IEnumerator DecreaseMood()
    {
        while (mood > 0)
        {
            if (GameManager.instance.gameStarted)
            {
                mood -= 0.2f;
            }

            moodBar.fillAmount = mood/100f;
            yield return new WaitForSeconds(1f);
        }
    }
}
