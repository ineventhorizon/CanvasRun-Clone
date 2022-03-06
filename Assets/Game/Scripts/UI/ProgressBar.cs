using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int maximum;
    [SerializeField] private Image imageToFill;
    public void SetCurrentFill(float current)
    {
        float fillAmount = ((float)current / (float)maximum);
        imageToFill.fillAmount = fillAmount;
    }
    public void SetMaxValue(int value)
    {
        maximum = value;
    }
    public void ResetProgressBar()
    {
        imageToFill.fillAmount = 0;
    }
}