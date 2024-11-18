/*
 * 
 * Author: Alfred Kang Jing Rui
 * Date: 16/11/2024
 * Purpose: Place Waypoint on Touch Script
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceWaypoint : MonoBehaviour
{
    // The GameObject to be placed as the waypoint
    public GameObject wayPointObject;

    // ARRaycastManager used for raycasting in AR space
    public ARRaycastManager arRaycastManager;

    // The Pose that holds the position and rotation of the waypoint
    private Pose placementPose;

    // The camera used for AR, to get the camera's forward direction
    public Camera arCamera;

    /// <summary>
    /// Updates the placement of the waypoint every frame.
    /// </summary>
    private void Update()
    {
        // Get the middle of the screen as the reference point for raycasting
        var screenMiddle = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        // List to store the AR raycast hits
        var hits = new List<ARRaycastHit>();

        // Perform a raycast from the middle of the screen and check for plane hits
        arRaycastManager.Raycast(screenMiddle, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            // If we hit a plane, update the placement pose with the hit position and rotation
            placementPose = hits[0].pose;

            // Get the camera's forward direction and adjust for horizontal rotation only
            var cameraForward = arCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            // Set the waypoint's rotation to face the camera's direction
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);

            // Enable the waypoint object and set its position and rotation
            wayPointObject.SetActive(true);
            wayPointObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            // If no plane is hit, hide the waypoint object
            wayPointObject.SetActive(false);
        }
    }
}
