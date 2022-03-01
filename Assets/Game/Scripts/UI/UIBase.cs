using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    //BASE CLASS FOR EVERY UI PANEL
    [SerializeField] protected CanvasGroup canvasGroup;
    public virtual void DisablePanel()
    {
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }

    public virtual void EnablePanel()
    {
        canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}