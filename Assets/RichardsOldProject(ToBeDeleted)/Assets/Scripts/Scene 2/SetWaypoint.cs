using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWaypoint : MonoBehaviour
{
    public GameObject Waypoint1;
    public GameObject Waypoint2;
    public GameObject CameraWaypoint;
    public GameObject Player;
    public GameObject CurrentWaypoint;
    public GameObject Artifact;
    public GameObject Door;

    void Update()
    {
        SelectWaypoint();
        //PlaceWaypoint();
        SetCameraWaypoint();
    }

    void SetCameraWaypoint()
    {
        if (Artifact.activeSelf == true)
        {
            CameraWaypoint.transform.position = Door.transform.position;

        }
        else if(CurrentWaypoint != null)
        {
            CameraWaypoint.transform.position = CurrentWaypoint.transform.position;
        }
        else
            CameraWaypoint.transform.position = Player.transform.position;
        
    }
    void SelectWaypoint()
    {
        if(Waypoint1 & Waypoint2 !=null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CurrentWaypoint = Waypoint1;
                print(Waypoint1.name+" Selected");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CurrentWaypoint = Waypoint2;
                print(Waypoint1.name+ " Selected");
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CurrentWaypoint = null;
                print("Cancel Selection");

            }
        }
        else { print("Waypoint not set in Inspector"); }


    }

    void PlaceWaypoint()
    {

        if (Input.GetMouseButtonDown(1))
        {
            if (CurrentWaypoint != null)
            {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    CurrentWaypoint.gameObject.transform.position = hit.point;
                    print(CurrentWaypoint.name + " position is now " + CurrentWaypoint.gameObject.transform.position);
                }
            }else print("No selected waypoint");
        }
        
    }

}
