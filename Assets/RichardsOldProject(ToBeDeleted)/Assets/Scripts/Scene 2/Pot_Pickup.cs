using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot_Pickup : MonoBehaviour
{
    public bool rawFood;
    // Start is called before the first frame update
    void Start()
    {
        rawFood = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<ItemPickup>().rawFood)
            {
                rawFood = true;
            }
        }
    }
}
