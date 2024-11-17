using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PathFinding : MonoBehaviour
{
    public ObjectSpawner spawner;
    
    public GameObject dest;
    public float moveSpd;

    public bool destSet = false;
    public bool socketSnap = false;
    
    [SerializeField] private GameObject playerCam;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("ObjectSpawner").GetComponent<ObjectSpawner>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (spawner.waypointAvail && !destSet)
        {
            dest = GameObject.FindGameObjectWithTag("Waypoint");
            if (dest != null)
            {
                destSet = true;
                Debug.Log("Dest Set!");
            }
        }

        if (spawner.waypointAvail && destSet && dest != null && !socketSnap)
        {
            transform.position = Vector3.Lerp(transform.position, dest.transform.position, moveSpd * Time.deltaTime);
            transform.LookAt(dest.transform.position);
        }
        else
        {
            destSet = false;
        }

        if (socketSnap)
        {
            Destroy(GameObject.FindGameObjectWithTag("Waypoint"));
            transform.LookAt(GameObject.FindGameObjectWithTag("Socket").transform.position);
        }
        
    }

    public void Grabbed()
    {
        if (destSet)
        {
            Destroy(GameObject.FindGameObjectWithTag("Waypoint"));
        }
    }
}
