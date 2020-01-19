using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySelection : MonoBehaviour
{

    public GameObject Waypoint1;
    public GameObject Waypoint2;
    public GameObject CurrentWaypoint;
    public MeshRenderer mesh;
    public bool blinking;
    public float Scale;
    public float blinkSpeed;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
    }
    void Update()
    {
        SelectWaypoint();
        PlaceWaypoint();
        ScaleOverTime();

    }

    void ScaleOverTime()
    {
        if (blinking)
        {
            gameObject.transform.localScale -= (new Vector3(0.5f, 0.5f, 0.5f)) * Time.deltaTime * blinkSpeed;
            Vector3 ScalingVector = new Vector3(Scale, Scale, Scale);
            if (gameObject.transform.localScale.x <= 0)
            {
                gameObject.transform.localScale = ScalingVector;
            }
        }
    }
    void SelectWaypoint()
    {
        if (Waypoint1 & Waypoint2 != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                mesh.enabled = true;
                CurrentWaypoint = Waypoint1;
                print("Sam Selected");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                mesh.enabled = true;
                CurrentWaypoint = Waypoint2;
                print("Gollum Selected");
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CurrentWaypoint = null;

                print("Cancel Selection");
                mesh.enabled = false;
            }
        }
        else { print("Waypoint not set in Inspector"); }


    }

    void PlaceWaypoint()
    {
        if (CurrentWaypoint != null)
        {
            gameObject.transform.position = CurrentWaypoint.transform.position+ new Vector3(0, 0.01f, 0);
        }
    }

}
