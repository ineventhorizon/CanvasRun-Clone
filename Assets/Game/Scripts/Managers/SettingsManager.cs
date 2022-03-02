using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SettingsManager : MonoSingleton<SettingsManager>
{
    [SerializeField, Expandable] CanvasSettings canvasSettings;
    public static CanvasSettings CanvasSettings => Instance.canvasSettings;
}
