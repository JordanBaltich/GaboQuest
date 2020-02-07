using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gollum_Hunger : MonoBehaviour
{
    public bool cookedFood;
    public bool hungry;
    [Range(1, 5)]
    public float hunger;
    // Update is called once per frame
    void Update()
    {
        if (hunger > 4f)
        {
            hungry = false;
        }
        else { hungry = true; }
    }


}
