/*
 * 
 * Author: Alfred Kang Jing Rui
 * Date: 16/11/2024
 * Purpose: Animal Pathfinding to Waypoint script
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

/// <summary>
/// Handles pathfinding logic for a game object, allowing it to move towards destinations dynamically.
/// </summary>
public class PathFinding : MonoBehaviour
{
    /// <summary>
    /// Reference to the object spawner that provides waypoints for the pathfinding.
    /// </summary>
    public ObjectSpawner spawner;

    /// <summary>
    /// The current destination for the object to move towards.
    /// </summary>
    public GameObject dest;

    /// <summary>
    /// The movement speed of the object.
    /// </summary>
    public float moveSpd;

    /// <summary>
    /// Indicates whether a destination has been set.
    /// </summary>
    public bool destSet = false;

    /// <summary>
    /// Indicates whether the object has snapped to a socket.
    /// </summary>
    public bool socketSnap = false;

    /// <summary>
    /// Reference to the player's camera.
    /// </summary>
    [SerializeField] private GameObject playerCam;

    /// <summary>
    /// Initializes the pathfinding component by locating required game objects.
    /// </summary>
    private void Start()
    {
        // Find and reference the object spawner and player camera by their tags.
        spawner = GameObject.FindGameObjectWithTag("ObjectSpawner").GetComponent<ObjectSpawner>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    
    /// <summary>
    /// Updates the pathfinding logic every frame, moving the object towards its destination if conditions are met.
    /// </summary>
    private void Update()
    {
        // Check if waypoints are available and a destination hasn't been set yet.
        if (spawner.waypointAvail && !destSet)
        {
            dest = GameObject.FindGameObjectWithTag("Waypoint");
            if (dest != null)
            {
                destSet = true;
                Debug.Log("Dest Set!");
            }
        }

        // Move towards the destination if it is set and the object hasn't snapped to a socket.
        if (spawner.waypointAvail && destSet && dest != null && !socketSnap)
        {
            transform.position = Vector3.Lerp(transform.position, dest.transform.position, moveSpd * Time.deltaTime);
            transform.LookAt(dest.transform.position);
        }
        else
        {
            destSet = false;
        }

        // If the object has snapped to a socket, destroy the destination and orient towards the socket.
        if (socketSnap)
        {
            if (destSet)
            {
                Destroy(dest); 
            }

            var socket = GameObject.FindGameObjectWithTag("Socket");
            if (socket != null)
            {
                transform.LookAt(socket.transform.position);
            }
        }
    }

    /// <summary>
    /// Handles logic when the object is grabbed by the player.
    /// </summary>
    public void Grabbed()
    {
        // Destroy the destination if it is set.
        if (destSet)
        {
            Destroy(dest);
        }
    }
}
