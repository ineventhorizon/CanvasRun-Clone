using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoSingleton<ObjectPooler>
{
    [SerializeField] private Transform parentSphere;
    [SerializeField] private CanvasSphere sphereObject;
    [SerializeField] public List<CanvasSphere> pooledSpheres;
    [SerializeField] public int projectilePoolAmount;
    private void Awake()
    {
        CreateSpheres();
    }

    private void CreateSpheres()
    {
        for (int i = 0; i < projectilePoolAmount; i++)
        {
            var obj = Instantiate(sphereObject, parentSphere);
            obj.Default();
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
                return pooledSpheres[i];
            }
        }
        var obj = Instantiate(sphereObject, parentSphere);
        obj.Default();
        pooledSpheres.Add(obj);
        return obj;
    }

    public void SetActiveSpheres()
    {
        for (int i = 0; i < pooledSpheres.Count; i++)
        {
            if (pooledSpheres[i].gameObject.activeInHierarchy)
            {
                pooledSpheres[i].Default();
                Debug.Log("Disabled");
            }
        }
    }
}
