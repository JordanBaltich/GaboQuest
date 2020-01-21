using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSelectLibee : MonoBehaviour
{
    public GameObject Player;
    public int CurrentLibeeIndex;

    public Transform CapturedLibees;
    public List<Transform> Normal;
    public List<Transform> Fire;
    public List<Transform> Water;
    public List<Transform> Grass;

    [SerializeField]
    public int[] LibeeCount;

    public GameObject UI_Normal;
    public GameObject UI_Fire;
    public GameObject UI_Water;
    public GameObject UI_Grass;
    private void Awake()
    {
        LibeeCount = new int[4];
        SortLibee();
    }

    void Update()
    {
        SelectLibees();
    }

    void SortLibee()
    {
        //Reset Libee Count
        for (int i = 0; i < LibeeCount.Length; i++)
        {
            LibeeCount[i] = 0;
            Normal.Clear();
            Fire.Clear();
            Water.Clear();
            Grass.Clear();
        }

        //Count Libee
        foreach (Transform child in CapturedLibees)
        {
            if (child.CompareTag("Normal"))
            {
                Normal.Add(child);
                LibeeCount[0] += 1;
            }
            if (child.CompareTag("Fire"))
            {
                Fire.Add(child);
                LibeeCount[1] += 1;
            }
            if (child.CompareTag("Water"))
            {
                Water.Add(child);
                LibeeCount[2] += 1;
            }
            if (child.CompareTag("Grass"))
            {
                Grass.Add(child);
                LibeeCount[3] += 1;
            }
        }
    }

    void SelectLibees()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SortLibee();

            if (CurrentLibeeIndex < LibeeCount.Length-1)
            {
                CurrentLibeeIndex += 1;
                if (LibeeCount[CurrentLibeeIndex] <= 0)
                {
                    CurrentLibeeIndex += 1;
                    if (CurrentLibeeIndex > LibeeCount.Length - 1)
                    {
                        CurrentLibeeIndex = 0;
                    }
                }
            }
            else CurrentLibeeIndex = 0;

        }
    }




    //                if (CapturedLibees.Count > 0)
    //            {

    //                CurrentWaypoint.transform.position = Waypoints[0].position;
    //                print(Waypoints[0].name + " Selected");
    //         else { print("Waypoint not set in Inspector"); }
    //}

    //            if (Input.GetKeyDown(KeyCode.Alpha2))
    //            {
    //                CurrentWaypoint = Waypoint2;
    //                print(Waypoints[0].name + " Selected");
    //            }
    //            if (Input.GetKeyDown(KeyCode.Escape))
    //            {
    //                CurrentWaypoint = null;
    //                print("Cancel Selection");

    //            }
    //void CycleWaypoint()
    //{

    //}

    //void PlaceWaypoint()
    //{

    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        if (CurrentWaypoint != null)
    //        {

    //            RaycastHit hit;
    //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                CurrentWaypoint.gameObject.transform.position = hit.point;
    //                print(CurrentWaypoint.name + " position is now " + CurrentWaypoint.gameObject.transform.position);
    //            }
    //        }
    //        else print("No selected waypoint");
    //    }

    //}

}
