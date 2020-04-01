using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitFX : MonoBehaviour
{
    [SerializeField] ParticleSystem _VFX1;
    [SerializeField] CapsuleCollider _Sphere;

    private void Awake()
    {
        _Sphere = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Normal")|| other.gameObject.CompareTag("Fire"))
        {
            _VFX1.transform.position = transform.position;
            _VFX1.Play(true);
        }
    }
    
}
