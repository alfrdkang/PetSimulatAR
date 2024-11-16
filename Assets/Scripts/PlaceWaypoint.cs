using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceWaypoint : MonoBehaviour
{
    public GameObject wayPointObject;
    public ARRaycastManager arRaycastManager;
    private Pose placementPose;
    public Camera arCamera;

    private void Update()
    {
        var screenMiddle = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        
        arRaycastManager.Raycast(screenMiddle, hits, TrackableType.Planes);
        if (hits.Count > 0)
        {
            placementPose = hits[0].pose;
            var cameraForward = arCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            wayPointObject.SetActive(true);
            wayPointObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            wayPointObject.SetActive(false);
        }
    }
}
