﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSelectLibee : MonoBehaviour
{
    public GameObject Player;
    public int CurrentLibeeIndex;

    public Transform CapturedLibees;
    public List<Transform> Normal;
    public List<Transform> Fire;

    public List<Transform>[] ammoPools;

    public Transform DeadLibeeStorage;
    public List<Transform> Dead;

    [SerializeField]
    public int[] LibeeCount;

    public GameObject UI_Normal;
    public GameObject UI_Fire;

    private void Awake()
    {
        LibeeCount = new int[2];
        SortLibee();
    }

    public void GatherDeadLibees(Transform DeadLibee)
    {
        DeadLibee.transform.position = DeadLibeeStorage.position;
        DeadLibee.transform.parent = DeadLibeeStorage;

        SortDeadLibees();
    }

    public void SortDeadLibees()
    {
        Dead.Clear();

        foreach (Transform t in DeadLibeeStorage)
        {
            Dead.Add(t);
        }
    }

    public void SortLibee()
    {
        //Reset Libee Count
        for (int i = 0; i < LibeeCount.Length; i++)
        {
            LibeeCount[i] = 0;
            Normal.Clear();
            Fire.Clear();

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
        }
    }


    public void SelectLibees()
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

    public int TotalLibees()
    {
        int count = 0;

        foreach (int libeeCount in LibeeCount)
        {
            count += libeeCount;
        }
        return count;
    }

    public List<Transform> CurrentAmmoPool()
    {
        if (CurrentLibeeIndex == 0)
        {
            return Normal;
        }
        else return Fire;
    }
}