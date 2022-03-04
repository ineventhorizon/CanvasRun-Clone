using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSphere : MonoBehaviour
{
    [SerializeField] public SphereCollider Collider;
    [SerializeField] public Rigidbody RigidBody;
    public bool IsTriggered = false;

    private void OnDisable()
    {
        this.RigidBody.isKinematic = true;
        this.Collider.isTrigger = true;
        IsTriggered = false;
    }
}
