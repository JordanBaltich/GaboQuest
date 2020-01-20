using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextToUI : MonoBehaviour
{
    public string silent = ""; 

    public string g_hungry = "Gollum wants food!";
    public string g_searching = "Shiny!";
    public string g_chasing = "Come back!";
    public string g_catching = "Yummy!";
    public string g_artifact = "My precious!";
    public string g_bargaining = "Give Gollum Food!";
    public string g_satisfied = "Have your stupid thing back!";

    public string s_checking = "On my way sir";
    public string s_limping = "Mr Frodo, I'm hurt";
    public string s_walking = "Let's go Mr Frodo";
    public string s_resting = "Need to lay down for a bit";
    public string s_cooking = "Right away Mr Frodo";
    public string s_waiting = "Food is ready Sir!";


    public TextMeshPro text;
    public string displayText;

    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<TextMeshPro>() != null)
        {
            text = GetComponent<TextMeshPro>();
        }

    }
    private void Update()
    {
        text.text = displayText;
        ResetText();
    }

    public void SetText(string line)
    {
        displayText = line;
        timer = 1.5f;

    }
    private void ResetText()
    {
        if (gameObject.tag != "Food")
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                displayText = silent;

            }
        }
    }
}
