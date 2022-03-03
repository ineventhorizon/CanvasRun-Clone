using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    //Will change it to hashset or dictionary.
    [SerializeField] private List<UI> uis;

    public void DisableUI(UIType type)
    {
        for(int i = 0; i < uis.Count; i++)
        {
            if(uis[i].Type == type)
            {
                uis[i].Ui.DisablePanel();
                break;
            }
        }
    }

    public void EnableUI(UIType type)
    {
        for (int i = 0; i < uis.Count; i++)
        {
            if (uis[i].Type == type)
            {
                uis[i].Ui.EnablePanel();
                break;
            }
        }
    }

}

[System.Serializable]
public class UI
{
    [SerializeField] public UIType Type;
    [SerializeField] public UIBase Ui;
}