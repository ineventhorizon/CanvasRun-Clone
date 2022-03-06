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
        this.transform.position = Vector3.zero;
        this.RigidBody.isKinematic = true;
        this.Collider.isTrigger = true;
        this.Collider.enabled = false;
        this.transform.SetParent(ObjectPooler.Instance.transform);
        this.gameObject.SetActive(false);
        this.IsTriggered = false;
    }
    public void Destroyed()
    {
        //explosionParticle.Play();
        this.Default();
    }
    public void Collected()
    {
        this.Collider.enabled = true;
    }
}
