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

            //if everything is empty, skip the rest
            for (int i = 0; i < LibeeCount.Length; i++)
            {
                isEmpty = true;

                if (LibeeCount[i] <= 0)
                {
                    isEmpty = false;
                }

                if (isEmpty)
                {
                    break;
                }
            }


            for (int i = 0; i < LibeeCount.Length; i++)
            {
                //if i tab, select next libee type
                if (CurrentLibeeIndex < LibeeCount.Length - 1)
                {
                    CurrentLibeeIndex += 1;

                    //if this libee type has none, select next
                    if (LibeeCount[CurrentLibeeIndex] <= 0)
                    {
                        CurrentLibeeIndex += 1;

                        // if the next index is out of range, select the first libee type
                        if (CurrentLibeeIndex > LibeeCount.Length - 1)
                        {
                            CurrentLibeeIndex = 0;
                        }
                    }
                }
            }

        }
    }
}
