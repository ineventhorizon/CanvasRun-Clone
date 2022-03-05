using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoot : MonoBehaviour
{

    [SerializeField] private Transform root;
    [SerializeField] private Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position = root.position + offset;
    }
}
