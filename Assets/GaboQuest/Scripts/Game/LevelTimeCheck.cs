using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LevelTimeCheck : MonoBehaviour
{
    public float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
}
