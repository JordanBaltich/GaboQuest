using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming_Camera : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] PlayerController playerController;     //Player Input Script
    [SerializeField] GameObject PlayerFront;        //Player's front offset
    [SerializeField] GameObject Crosshair_unlocked;     //VFX
    [SerializeField] GameObject Crosshair_locked;       //VFX

    [SerializeField] float time;
    [SerializeField] float aimSensitivity;      //Desired Sensitivity
    [SerializeField] float newSensitivity;      //Current Sensitivity

    [SerializeField] float aimRadius;   //Aim Radius
    [SerializeField] Vector3 aimDir;    //Normalized Aiming Input

    [Range(1, 3)]
    [SerializeField] int aimMode;   //Different Aim modes
    [Range (1,15)]
    [SerializeField] float aimMode2Speed;   //Speed of the crosshair

    [SerializeField] EnemyLocations Enemies;    //Script to read all enemies
    [SerializeField] float enemyDist;    //Distance of the closet Enemy
    [SerializeField] float playerDist;    //Distance of the player from crosshair
    [SerializeField] bool Locking;


    private void Awake()
    {
        playerController = Player.GetComponent<PlayerController>();
        Enemies = GetComponentInChildren<EnemyLocations>();
    }

    void FixedUpdate()
    {
        playerDist = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        AimingPoint();
        //LookRotate();
        AimAssist();
        transform.position = new Vector3(transform.position.x, Player.transform.position.y - 0.9f, transform.position.z);
    }

    //AimAssist chases an enemy
    void AimAssist()
    {
        time += Time.fixedDeltaTime;
        if (time >= 1f)
        {
            Locking = false;
        }

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
    
    //AimPoint enable moving of the crosshair
    void AimingPoint()
    {
        Cursor.visible = false;

        if (playerController.player.GetButton("Aim"))
        {

            Crosshair_unlocked.SetActive(true);

            if (aimMode == 1)
            {

                gameObject.transform.position = new Vector3(Player.transform.position.x + DragPoint().x, 0, Player.transform.position.z + DragPoint().z);
            }
            if (aimMode == 2)
            {

                Vector3 directon = transform.position - Player.transform.position;
                float distance = directon.magnitude;

                if (Mathf.Abs(distance) <= aimRadius)
                {
                    gameObject.transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(DragPoint().x, 0, DragPoint().z), aimMode2Speed * Time.deltaTime);
                }
                if (Mathf.Abs(distance) > aimRadius - 0.3f)
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
            gameObject.transform.position = new Vector3(PlayerFront.transform.position.x, 0f,  PlayerFront.transform.position.z);
            Crosshair_unlocked.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
            
    }

    //DragPoint read the input of controller/mouse
    public Vector3 DragPoint()
    {

        aimDir = new Vector3((playerController.player.GetAxis("R_Horizontal")), 0f, (playerController.player.GetAxis("R_Vertical")));

        if(playerDist> aimRadius * 0.8f)
        {
            newSensitivity = aimSensitivity * 0.2f;
        }
        else
        {
            newSensitivity = aimSensitivity;
        }
        aimDir *= newSensitivity;

        return aimDir;
    }

}

