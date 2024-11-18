/*
 * 
 * Author: Alfred Kang Jing Rui
 * Date: 17/11/2024
 * Purpose: Removes waypoint when animal reaches it
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            Destroy(gameObject);
        }
    }
}
