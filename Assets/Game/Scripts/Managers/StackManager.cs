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

    public void DecreaseStackCount()
    {
        stack.StackCount--;
    }
    public IEnumerator CasinoFinalRoutine(Transform newPosition)
    {
        //PRE STATE
        Observer.MoveStackToPosition?.Invoke(newPosition);
        while (stack.StackCount > 0)
        {
            yield return null;
        }

        GameManager.Instance.IsGameEnded = true;
    }


    public void HandleGate(GateType type, int amount)
    {
        switch (type)
        {
            case GateType.WIDTH:
                if (Width + amount <= 0)
                {
                    GameManager.Instance.GameOver();
                    return;
                }
                stack.UpdateWidth(amount);
                break;
            case GateType.LENGTH:
                if (Length + amount <= 0)
                {
                    GameManager.Instance.GameOver();
                    return;
                }
                stack.UpdateLength(amount);
                break;
            default:
                break;
        }
    }

    public void HandleObstacle(ObstacleType type ,int amount)
    {
        switch (type)
        {
            case ObstacleType.THIN:
                stack.UpdateWidth(-1);
                break;
            case ObstacleType.POOL:
                break;
            case ObstacleType.WALL:
                break;
            default:
                break;
        }
    }
}
