using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        Observer.StartGame?.Invoke();
        Observer.HandleCanvasLimits?.Invoke();
    }
}
