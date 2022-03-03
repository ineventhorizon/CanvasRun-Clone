using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinObstacle : MonoBehaviour
{
    private ObstacleType type = ObstacleType.THIN;
    private bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        var collidePoint = this.transform.position - other.attachedRigidbody.transform.position;
        if (other.CompareTag("Sphere") && !isTriggered && !(collidePoint.z <= 0))
        {
            Debug.Log(collidePoint);
            Debug.Log(other.name);
            isTriggered = true;
            StackManager.Instance.HandleObstacle(type, -1);
        }
    }
}
