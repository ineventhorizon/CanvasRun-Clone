using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyEnabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sphere"))
        {
            var sphere = other.GetComponent<CanvasSphere>();
            sphere.RigidBody.isKinematic = false;
            sphere.Collider.isTrigger = false;
            sphere.transform.SetParent(null);

        }
    }
}
