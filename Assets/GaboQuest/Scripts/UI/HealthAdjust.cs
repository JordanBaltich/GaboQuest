using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAdjust : MonoBehaviour
{

    [SerializeField]
    Sprite[] GaboHPImages = new Sprite[6];
    public Health health;
    [SerializeField]
    Image CurrentGaboImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentGaboImage.sprite = GaboHPImages[health.currentHealth];
    }


}
