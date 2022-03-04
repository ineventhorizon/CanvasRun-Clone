using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    private bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            if (isTriggered) return;
            isTriggered = true;
            TriggerFinal();
            
        }
    }

    virtual protected void TriggerFinal()
    {
        Debug.Log("Final");
    }
}
