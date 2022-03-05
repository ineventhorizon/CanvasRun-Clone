using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CanvasSettings", menuName = "ScriptableObjects/CanvasSettings", order = 1)]
public class CanvasSettings : ScriptableObject
{
    public float forwardSpeed;
    public float sideMovementSensivity;
    public float sideLerpValue;
    public float forwardLerpValue;
    public float gap;
}
