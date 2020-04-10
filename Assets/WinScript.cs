using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;

    [SerializeField]
    int playedID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playedID)
        {
            if (!winCanvas.activeInHierarchy)
            {
                winCanvas.SetActive(true);
            }
        }
    }
}
