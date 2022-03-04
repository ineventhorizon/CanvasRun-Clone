using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Observer
{
    public static UnityAction StartGame;
    public static UnityAction HandleCanvasLimits;
    public static UnityAction StackChanged;
    public static UnityAction<Transform> MoveStackToPosition;
}
