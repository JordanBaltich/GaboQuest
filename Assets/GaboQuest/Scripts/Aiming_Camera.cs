using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming_Camera : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerFront;
    public float aimSensitivity;

    public float aimRadius;
    public Vector3 CursorPos;
    public Vector3 aimDir;
    public Vector3 clampedDir;
    [Range(1, 3)]
    public int aimMode;

    [Range (1,15)]
    public float aimMode2Speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        AimingPoint();
        LookRotate();
    }
    void AimingPoint()
    {
        Cursor.visible = false;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.None;

            if (aimMode == 1)
            {

                gameObject.transform.position = new Vector3(Player.transform.position.x + DragPoint().x, 0f, Player.transform.position.z + DragPoint().z);
            }
            if (aimMode == 2)
            {
                if (Vector3.Distance(gameObject.transform.position, Player.transform.position) <= aimRadius)
                    gameObject.transform.position += new Vector3(DragPoint().x, 0f, DragPoint().z) * Time.deltaTime * aimMode2Speed;
            }
            if(aimMode == 3)
            {
                gameObject.transform.position = new Vector3(Player.transform.position.x + DragPoint().x + DragPoint().x * Time.deltaTime * aimMode2Speed, 0f, Player.transform.position.z + DragPoint().z + DragPoint().z * Time.deltaTime * aimMode2Speed);
            }
        }
        else
        {
            //gameObject.transform.position = new Vector3(Player.transform.position.x, 0f, Player.transform.position.z);
            gameObject.transform.position = new Vector3(PlayerFront.transform.position.x, 0f,  PlayerFront.transform.position.z);

            Cursor.lockState = CursorLockMode.Locked;
        }
            
    }
    public Vector3 DragPoint()
    {
        // Adjusting mouse position to screen center
        CursorPos = new Vector3((Input.mousePosition.x - Screen.width / 2), 0f, (Input.mousePosition.y - Screen.height/2));
        
        // Get percentage to screen
        aimDir = new Vector3 (CursorPos.x / Screen.width, 0f, CursorPos.z / Screen.height);
        aimDir *= aimSensitivity;
        clampedDir = new Vector3(Mathf.Clamp(aimDir.x, -aimRadius, aimRadius), 0f, Mathf.Clamp(aimDir.z, -aimRadius, aimRadius));
        return Vector3.ClampMagnitude(clampedDir, aimRadius);
    }
    public void LookRotate()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, 0f,Player.transform.position.z));
    }
}

