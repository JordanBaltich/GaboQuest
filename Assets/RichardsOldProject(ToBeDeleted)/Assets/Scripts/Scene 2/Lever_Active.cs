using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_Active : MonoBehaviour
{
    public RotateTo Door;
    // Start is called before the first frame update
    void Start()
    {
        //Door = GetComponent<RotateTo>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.activeSelf)
        {
            Door.active = true;
        }
    }
}
