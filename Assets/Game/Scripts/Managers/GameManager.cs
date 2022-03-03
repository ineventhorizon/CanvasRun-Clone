using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField, ReadOnly] public GameState CurrentGameState = GameState.MENU;
    private void Start()
    {
        //StartGame();
    }
    public void StartGame()
    {
        UIManager.Instance.DisableUI(UIType.STARTSCREEN);
        UIManager.Instance.EnableUI(UIType.INGAMESCREEN);
        CurrentGameState = GameState.GAMEPLAY;
        Observer.StartGame?.Invoke();
        Observer.HandleCanvasLimits?.Invoke();
    }
    public void GameOver()
    {
        CurrentGameState = GameState.MENU;
        UIManager.Instance.DisableUI(UIType.INGAMESCREEN);
        UIManager.Instance.EnableUI(UIType.FAILSCREEN);
        Debug.Log("Game Over");

    }
    public void RestartGame()
    {
        CurrentGameState = GameState.MENU;
        UIManager.Instance.DisableUI(UIType.FAILSCREEN);
        UIManager.Instance.EnableUI(UIType.STARTSCREEN);
        MySceneManager.Instance.RestartActiveScene();
    }
}
