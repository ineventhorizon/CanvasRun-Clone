using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoSingleton<ObjectPooler>
{
    [SerializeField] private Transform parentSphere;
    [SerializeField] private CanvasSphere sphereObject;
    [SerializeField] public List<CanvasSphere> pooledSpheres;
    [SerializeField] public int projectilePoolAmount;
    void Start()
    {
        CreateSpheres();
    }

    private void CreateSpheres()
    {
        for (int i = 0; i < projectilePoolAmount; i++)
        {
            var obj = Instantiate(sphereObject, parentSphere);
            pooledSpheres.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }
    public CanvasSphere GetPooledSphere()
    {
        for (int i = 0; i < pooledSpheres.Count; i++)
        {
            if (!pooledSpheres[i].gameObject.activeInHierarchy)
            {
                pooledSpheres[i].Default();
                return pooledSpheres[i];
            }
        }
        var obj = Instantiate(sphereObject, parentSphere);
        pooledSpheres.Add(obj);
        return obj;
    }

    public void SetActiveSpheres()
    {
        for (int i = 0; i < pooledSpheres.Count; i++)
        {
            if (pooledSpheres[i].gameObject.activeInHierarchy)
            {
                pooledSpheres[i].gameObject.transform.SetParent(this.transform);
                pooledSpheres[i].gameObject.SetActive(false);
                pooledSpheres[i].RigidBody.isKinematic = true;
                pooledSpheres[i].Collider.isTrigger = true;
                pooledSpheres[i].IsTriggered = false;
                Debug.Log("Disabled");
            }
        }
    }
}
