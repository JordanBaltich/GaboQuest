using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] int libeeLayer;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == libeeLayer)
        {

        }
    }
}
