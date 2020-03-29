using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField]
    Sprite[] LibeeTypeImages = new Sprite[2];
    [SerializeField]
    int[] counts = new int[2];

    [SerializeField]
    TextMeshProUGUI NumberOfLibees;
    [SerializeField]
    Image CurrentLibeeImage;

    public SortSelectLibee LibeeSorter;

    int selectedLibeeType;

    private void Awake()
    {
        
    }

    private void Start()
    {
        GetInfo();
    }

    private void Update()
    {
        GetInfo();
    }

    void GetInfo()
    {
        for (int i = 0; i < counts.Length; i++)
        {
            counts[i] = LibeeSorter.LibeeCount[i];
        }

        selectedLibeeType = LibeeSorter.CurrentLibeeIndex;

        CurrentLibeeImage.sprite = LibeeTypeImages[selectedLibeeType];
        NumberOfLibees.text = counts[selectedLibeeType].ToString();
    }
}
