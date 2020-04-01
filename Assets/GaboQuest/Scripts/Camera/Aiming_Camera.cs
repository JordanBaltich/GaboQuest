using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming_Camera : MonoBehaviour
{
    public GameObject Player;
    PlayerController playerController;
    public GameObject PlayerFront;
    public GameObject Crosshair_unlocked;
    public GameObject Crosshair_locked;

    float time;
    public float aimSensitivity;

    public float aimRadius;
    public Vector3 CursorPos;
    public Vector3 aimDir;
    public Vector3 clampedDir;
    [Range(1, 3)]
    public int aimMode;

    [Range (1,15)]
    public float aimMode2Speed;

    public EnemyLocations Enemies;
    public float enemyDist;
    public float playerDist;
    public bool Locking;


    private void Awake()
    {
        playerController = Player.GetComponent<PlayerController>();
        Enemies = GetComponentInChildren<EnemyLocations>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerDist = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        AimingPoint();
        //LookRotate();
        AimAssist();
        transform.position = new Vector3(transform.position.x, Player.transform.position.y - 0.9f, transform.position.z);
    }
    void AimAssist()
    {

        time += Time.fixedDeltaTime;
        if (time >= 1f)
        {
            Locking = false;
        }
        //Enemies.FindClosestEnemy().position
        if (playerController.player.GetButton("Aim"))
        {
            enemyDist = Vector3.Distance(gameObject.transform.position, Enemies.FindClosestEnemy().position);
            if (enemyDist <= 3f && Locking == false)
            {
                if (Vector3.Distance(gameObject.transform.position, Player.transform.position) <= (aimRadius-1))
                {
                    gameObject.transform.position = Enemies.FindClosestEnemy().position;

                    Crosshair_locked.SetActive(true);
                    time = 0f;
                    time += Time.fixedDeltaTime;
                    if (time >= 1f)
                    {
                        Locking = true;
                    }
                }
                else
                    Crosshair_locked.SetActive(false);
            }
            else
                Crosshair_locked.SetActive(false);
        }
        else
            Crosshair_locked.SetActive(false);

    }
    void AimingPoint()
    {
        Cursor.visible = false;

        if (playerController.player.GetButton("Aim"))
        {
            //Cursor.lockState = CursorLockMode.None;
            Crosshair_unlocked.SetActive(true);
            if (aimMode == 1)
            {

                gameObject.transform.position = new Vector3(Player.transform.position.x + DragPoint().x, 0, Player.transform.position.z + DragPoint().z);
            }
            if (aimMode == 2)
            {
                //if (Vector3.Distance(gameObject.transform.position, Player.transform.position) <= aimRadius)
                //    gameObject.transform.position += new Vector3(Player.transform.position.x + DragPoint().x, 0f, Player.transform.position.z + DragPoint().z) * Time.deltaTime * aimMode2Speed;

                Vector3 directon = transform.position - Player.transform.position;
                float distance = directon.magnitude;

                if (Mathf.Abs(distance) <= aimRadius)
                {
                    gameObject.transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(DragPoint().x, 0, DragPoint().z), aimMode2Speed * Time.deltaTime);
                }
                else if (Mathf.Abs(distance) > aimRadius - 0.1f)
                {

                    Vector3 fromPlayer = transform.position - Player.transform.position;
                    fromPlayer *= aimRadius / Vector3.Distance(transform.position, Player.transform.position);
                    transform.position = new Vector3(Player.transform.position.x + fromPlayer.x, 0, Player.transform.position.z + fromPlayer.z);
                }


            }
            if (aimMode == 3)
            {
                gameObject.transform.position = new Vector3(Player.transform.position.x + DragPoint().x + DragPoint().x * Time.deltaTime * aimMode2Speed, 0, Player.transform.position.z + DragPoint().z + DragPoint().z * Time.deltaTime * aimMode2Speed);
            }
        }
        else
        {
            //gameObject.transform.position = new Vector3(Player.transform.position.x, 0f, Player.transform.position.z);
            gameObject.transform.position = new Vector3(PlayerFront.transform.position.x, 0f,  PlayerFront.transform.position.z);
            Crosshair_unlocked.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
            
    }
    public Vector3 DragPoint()
    {
        // Adjusting mouse position to screen center
        //CursorPos = new Vector3((Input.mousePosition.x - Screen.width / 2), 0f, (Input.mousePosition.y - Screen.height/2));

        CursorPos = new Vector3((playerController.player.GetAxis("R_Horizontal")), 0f, (playerController.player.GetAxis("R_Vertical")));

        // Get percentage to screen
        //aimDir = new Vector3 (CursorPos.x / Screen.width, 0f, CursorPos.z / Screen.height);

        aimDir = new Vector3(CursorPos.x , 0, CursorPos.z);

        aimDir *= aimSensitivity;

        //clampedDir = new Vector3(Mathf.Clamp(aimDir.x, -aimRadius, aimRadius), 0f, Mathf.Clamp(aimDir.z, -aimRadius, aimRadius));
        //return Vector3.ClampMagnitude(clampedDir, aimRadius);
        return aimDir;
    }
    public void LookRotate()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, 0f,Player.transform.position.z));
    }
}

