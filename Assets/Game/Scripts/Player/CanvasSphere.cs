using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSphere : MonoBehaviour
{
    [SerializeField] public SphereCollider Collider;
    [SerializeField] public Rigidbody RigidBody;
    public bool IsTriggered = false;

    //Sets sphere to it's default values.
    public void Default()
    {
        this.RigidBody.isKinematic = true;
        this.Collider.isTrigger = true;
        this.transform.SetParent(ObjectPooler.Instance.transform);
        this.gameObject.SetActive(false);
        this.IsTriggered = false;
    }
    public IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(10);
        this.IsTriggered = true;
        StackManager.Instance.DecreaseStackCount();
    }
}
