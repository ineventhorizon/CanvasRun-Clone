using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : UIBase
{
    public void StartGame()
    {
        Debug.Log("Clicked, starting game");
        GameManager.Instance.StartGame();
    }
}
