using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSelectLibee : MonoBehaviour
{
    public GameObject Player;
    public int CurrentLibeeIndex;

    public Transform CapturedLibees;
    public List<Transform> Normal;
    public List<Transform> Fire;
    public List<Transform> Water;
    public List<Transform> Grass;

    [SerializeField]
    public int[] LibeeCount;

    public GameObject UI_Normal;
    public GameObject UI_Fire;
    public GameObject UI_Water;
    public GameObject UI_Grass;
    private void Awake()
    {
        LibeeCount = new int[4];
        SortLibee();
    }

    void Update()
    {
        SelectLibees();
    }

    public void SortLibee()
    {
        //Reset Libee Count
        for (int i = 0; i < LibeeCount.Length; i++)
        {
            LibeeCount[i] = 0;
            Normal.Clear();
            Fire.Clear();
            Water.Clear();
            Grass.Clear();
        }

        //Count Libee
        foreach (Transform child in CapturedLibees)
        {
            if (child.CompareTag("Normal"))
            {
                Normal.Add(child);
                LibeeCount[0] += 1;
            }
            if (child.CompareTag("Fire"))
            {
                Fire.Add(child);
                LibeeCount[1] += 1;
            }
            if (child.CompareTag("Water"))
            {
                Water.Add(child);
                LibeeCount[2] += 1;
            }
            if (child.CompareTag("Grass"))
            {
                Grass.Add(child);
                LibeeCount[3] += 1;
            }
        }
    }


    void SelectLibees()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

            SortLibee();

            //check above current index
            for (int i = CurrentLibeeIndex; i < LibeeCount.Length - 1; i++)
            {
                //break out of loop if on last element of array
                if (i == LibeeCount.Length - 1)
                    break;

                //switches to next ammo
                if (LibeeCount[i + 1] > 0)
                {
                    CurrentLibeeIndex = i + 1;
                    return;
                }
            }

            //check below current index
            for (int i = 0; i < LibeeCount.Length - 1; i++)
            {
                //keeps current index if player has no other ammo type
                if (i == CurrentLibeeIndex)
                {
                    return;
                }
                //switches to next ammo
                if (LibeeCount[i] > 0)
                {
                    CurrentLibeeIndex = i;
                    return;
                }
            }
        }
    }
}