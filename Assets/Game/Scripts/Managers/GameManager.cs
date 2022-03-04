using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField, ReadOnly] public GameState CurrentGameState = GameState.MENU;
    public void StartGame()
    {
        UIManager.Instance.StartScreen.DisablePanel();
        UIManager.Instance.InGameScreen.EnablePanel();
        CurrentGameState = GameState.GAMEPLAY;
        Observer.StartGame?.Invoke();
        Observer.HandleCanvasLimits?.Invoke();
    }
    public void GameOver()
    {
        CurrentGameState = GameState.MENU;
        UIManager.Instance.InGameScreen.DisablePanel();
        UIManager.Instance.FailScreen.EnablePanel();
        Debug.Log("Game Over");

    }
    public void RestartGame()
    {
        CurrentGameState = GameState.MENU;
        UIManager.Instance.FailScreen.DisablePanel();
        UIManager.Instance.StartScreen.EnablePanel();
        MySceneManager.Instance.RestartActiveScene();
    }

    public void FinalGame(FinalType type ,Transform newPosition)
    {
        CurrentGameState = GameState.FINAL;
        CameraManager.Instance.SwitchCam("FinalCam");
        switch (type)
        {
            case FinalType.CASINO:
                Observer.MoveStackToPosition(newPosition);
                break;
            case FinalType.FLAT:
                break;
            default:
                break;
        }
    }
}
