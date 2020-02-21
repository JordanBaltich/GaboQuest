using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Button : MonoBehaviour
{
    public UnityEvent wallMoveUpController;
    public UnityEvent wallMoveDownController;
    public bool buttonPressed;

    private void Update()
    {
        if (buttonPressed == true)
        {
            wallMoveDownController.Invoke();

        }
        else
        {
            wallMoveUpController.Invoke();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            buttonPressed = !buttonPressed;
        }
    }
}
