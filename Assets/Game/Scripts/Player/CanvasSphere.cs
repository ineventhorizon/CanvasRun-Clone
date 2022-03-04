using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSphere : MonoBehaviour
{
    [SerializeField] public SphereCollider Collider;
    [SerializeField] public Rigidbody RigidBody;
    public bool IsTriggered = false;
    public IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(10);
        this.IsTriggered = true;
        StackManager.Instance.DecreaseStackCount();
    }
}
