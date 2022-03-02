using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CanvasStack : MonoBehaviour
{
    [SerializeField, ReadOnly] private List<CanvasSphere> stack;
    [SerializeField] private Transform rootPoint;
    //x, y
    [SerializeField] private int width, length;
    private float stackGap => SettingsManager.CanvasSettings.gap;
    private Vector3 offset;
    private List<Vector3> oldPositions;
    public int Width {
        get { return width; }
        set { width = value; }
    }
    public int Length
    {
        get { return length; }
        set { length = value; }
    }
    private int index;
    private void OnEnable()
    {
        Observer.StartGame += FirstLayout;
    }
    private void FirstLayout()
    {
        for (int i = 0; i < width; i++)
        {
            var x = ((width / 2) - i) * stackGap;
            for (int j = 0; j < length; j++)
            {
                var z = (rootPoint.position.z - j) * stackGap;
                var sphere = ObjectPooler.Instance.GetPooledSphere();
                sphere.gameObject.SetActive(true);
                var newPos = new Vector3(x, sphere.transform.localPosition.y, z);
                sphere.transform.localPosition = rootPoint.transform.localPosition + newPos;
                sphere.transform.SetParent(this.transform);
                stack.Add(sphere);
                //oldPositions.Add(sphere.transform.position);

                Debug.Log($"{x}, {z}");
            }
        }

       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOffSet();
        StackMovement();
    }
    private void UpdateOffSet()
    {
        offset = Vector3.back * stackGap;
        //offsetFirst = Vector3.back * firstStackGap;
    }

    private void StackMovement()
    {
       for(int i = 0; i < width; i++)
        {
            for(int j = length-1; j >= 1; j--)
            {
                stack[GetIndex(i, j)].transform.position = Vector3.Lerp(stack[GetIndex(i, j)].transform.position, stack[GetIndex(i, j-1)].transform.position + offset, 0.3f);
            }
        }
    }

    private int GetIndex(int i, int j)
    {
        return i*length + j;
    }
}
