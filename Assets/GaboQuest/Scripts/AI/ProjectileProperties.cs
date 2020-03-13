using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileScriptableObject", order = 1)]
public class ProjectileProperties : ScriptableObject
{
    [Range(.3f, 20f)]
    public float Force;
}