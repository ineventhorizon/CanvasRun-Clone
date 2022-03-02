using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : SceneBasedMonoSingleton<StackManager>
{
    [SerializeField] private CanvasStack stack;
    public int Width
    {
        get { return stack.Width; }
        set { stack.Width = value; }
    }

    public int Length
    {
        get { return stack.Length; }
        set { stack.Length = value; }
    }

    public void HandleGate(GateType type, int amount)
    {
        switch (type)
        {
            case GateType.WIDTH:
                stack.UpdateWidth(amount);
                break;
            case GateType.LENGHT:
                stack.UpdateLength(amount);
                break;
            default:
                break;
        }
    }

    public void HandleObstacle(CanvasSphere sphere)
    {
        stack.RemoveFromStack(sphere);
        
    }
}
