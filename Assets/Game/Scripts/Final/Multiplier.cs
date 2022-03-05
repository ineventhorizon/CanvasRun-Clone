using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] private int multiplier;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            var sphere = other.GetComponent<CanvasSphere>();
            if (sphere.IsTriggered) return;
            sphere.IsTriggered = true;
            UIManager.Instance.InGameScreen.SetScore(multiplier*10);
            StackManager.Instance.DecreaseStackCount();
            //StartCoroutine(sphere.LifeTimeRoutine());
            sphere.Default();
        }
    }
}
