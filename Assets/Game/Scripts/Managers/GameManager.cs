using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField, ReadOnly] public GameState CurrentGameState = GameState.MENU;
    public bool IsGameEnded = false;
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

    [Button]
    public void NextLevel()
    {
        CurrentGameState = GameState.MENU;
        UIManager.Instance.InGameScreen.DisablePanel();
        UIManager.Instance.WinScreen.DisablePanel();
        UIManager.Instance.StartScreen.EnablePanel();
        MySceneManager.Instance.LoadNextLevel();
    }

    public void FinalGame(FinalType type ,Transform newPosition)
    {
        CurrentGameState = GameState.FINAL;
        CameraManager.Instance.SwitchCam("FinalCam");
        switch (type)
        {
            case FinalType.CASINO:
                StartCoroutine(FinalRoutine(StartCoroutine(StackManager.Instance.CasinoFinalRoutine(newPosition))));

                break;
            case FinalType.FLAT:
                break;
            default:
                break;
        }
    }

    private IEnumerator FinalRoutine(Coroutine finalRoutine)
    {
        yield return finalRoutine;
        UIManager.Instance.WinScreen.EnablePanel();
    }
}
