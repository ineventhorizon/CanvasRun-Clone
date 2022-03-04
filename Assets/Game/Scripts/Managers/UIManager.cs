using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] public InGameScreen InGameScreen;
    [SerializeField] public StartScreen StartScreen;
    [SerializeField] public FailScreen FailScreen;
    [SerializeField] public WinScreen WinScreen;

}

[System.Serializable]
public class UI
{
    [SerializeField] public UIType Type;
    [SerializeField] public UIBase Ui;
}