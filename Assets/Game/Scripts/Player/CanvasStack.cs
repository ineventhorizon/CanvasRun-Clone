using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CanvasStack : MonoBehaviour
{
    [SerializeField, ReadOnly] private List<CanvasSphere> stack;
    [SerializeField] private Transform rootPoint;
    //x, y
    [SerializeField] private int width, lenght;
    private int index;

    void Start()
    {
        for(int i = 0; i < width; i++)
        {
            var x = ((width / 2) - i) * SettingsManager.CanvasSettings.gap;
            for(int j = 0; j < lenght; j++)
            {
                var z = (rootPoint.position.z - j)*SettingsManager.CanvasSettings.gap;
                var sphere = ObjectPooler.Instance.GetPooledSphere();
                sphere.gameObject.SetActive(true);
                var newPos = new Vector3(x, sphere.transform.localPosition.y, z);
                sphere.transform.localPosition = rootPoint.transform.localPosition + newPos;
                sphere.transform.SetParent(this.transform);
                stack.Add(sphere);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TODO
       // for(int i = 0; i < width; i++)
       // {
       //     for(int j = 1;j< lenght; j++)
       //     {
       //
       //         Debug.Log($"{GetIndex(i, j)} {GetIndex(i, j - 1)}");
       //         stack[GetIndex(i, j)].transform.position = Vector3.Lerp(stack[GetIndex(i, j)].transform.position, stack[GetIndex(i, j-1)].transform.position, 0.8f);
       //     }
       // }
    }

    private int GetIndex(int i, int j)
    {
        return i + width * j;
    }
}
