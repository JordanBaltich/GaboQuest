using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AIData", menuName = "ScriptableObjects/AIScriptableObject", order = 1)]
public class AgentProperties : ScriptableObject
{
    #region HealthStatus
    [Range (0,100)]
    public int MaxHealth = 75;
    [Range(0, 100)]
    public int MaxStamina = 100;
    #endregion HealthStatus


    #region Attack
    [Range(0, 50)]
    public int Damage = 10;
    [Range(0, 20)]
    public float AttackRange;
    #endregion Attack


    #region Movement
    [Header("Agent Speed")]
    [Range(1, 20)]
    public float Speed = 5;
    [Range(1, 50)]
    public float RunSpeed = 10;

    [Range(1, 1440)]
    public float AngularSpeed;
    [Range(.5f, 20f)]
    public float Acceleration;
    [Range(.5f, 20f)]
    public float Deceleration;

    [Range(1, 1440)]
    public float RunAngularSpeed;
    [Range(1, 20)]
    public float RunAcceleration;

    #endregion Movement


    #region Sightline
    [Range(1, 30)]
    public int VisionRadius = 10;
    [Range(0, 360)]
    public int VisionAngle = 25;
    #endregion Sightline


    #region Behaviour
    [Range(0, 100)]
    public int WanderChance = 10;
    [Range(0, 10)]
    public int PatrolWait = 2;
    #endregion Behaviour



}