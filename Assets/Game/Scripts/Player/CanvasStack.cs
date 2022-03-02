using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CanvasStack : MonoBehaviour
{
    [SerializeField, ReadOnly] private List<CanvasSphere> stack;
    [SerializeField] private Transform rootPoint;
    //x, y
    [SerializeField] private int maxWidth, maxLength;
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
    //private int index;
    private void OnEnable()
    {
        Observer.StartGame += FirstLayout;
    }
    private void FirstLayout()
    {
        for (int i = 0; i < width; i++)
        {
            var x = (i - (width / 2) ) * stackGap;
            for (int j = 0; j < length; j++)
            {
                var z = (rootPoint.position.z - j) * stackGap;
                var sphere = ObjectPooler.Instance.GetPooledSphere();
                sphere.gameObject.SetActive(true);
                var newPos = new Vector3(x, sphere.transform.localPosition.y, z);
                sphere.transform.localPosition = rootPoint.transform.localPosition + newPos;
                sphere.transform.SetParent(this.transform);
                stack.Add(sphere);
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
                //stack[GetIndex(i, j)].transform.position = Vector3.Lerp(stack[GetIndex(i, j)].transform.position, stack[GetIndex(i, j-1)].transform.position + offset, 0.45f);
                var tempPos = stack[GetIndex(i, j)].transform.position;
                var newPosX = stack[GetIndex(i, j - 1)].transform.position.x;
                var newPosZ = stack[GetIndex(i, j - 1)].transform.position.z;
                tempPos.x = Mathf.Lerp(tempPos.x, newPosX, 0.45f);
                tempPos.z = newPosZ + offset.z;
                stack[GetIndex(i, j)].transform.position = tempPos;
            }
        }
    }
    private int SphereIndex(CanvasSphere sphere)
    {
        return stack.IndexOf(sphere);
    }

    private int GetIndex(int i, int j)
    {
        return i*length + j;
    }

    public void UpdateWidth(int amount)
    {
        
        width += amount;
        Debug.Log(width);
        for (int i = width -amount; i < width; i++)
        {
            var x = (i - (width / 2)) * stackGap;
            for (int j = 0; j < length; j++)
            {
                var z = (rootPoint.position.z - j) * stackGap;
                var sphere = ObjectPooler.Instance.GetPooledSphere();
                sphere.gameObject.SetActive(true);
                var newPos = new Vector3(x, sphere.transform.position.y, 0);
                sphere.transform.position = rootPoint.transform.position + newPos;
                sphere.transform.SetParent(this.transform);
                stack.Add(sphere);
            }
        }
    }

    public void UpdateLength(int amount)
    {
        length += amount;
        for (int i = 0; i < width; i++)
        {
            for(int j = length - amount; j < length; j++)
            {
                var sphere = ObjectPooler.Instance.GetPooledSphere();
                sphere.gameObject.SetActive(true);
                var newPos = new Vector3(0, sphere.transform.position.y, stack[GetIndex(i, j - 1)].transform.position.z);
                sphere.transform.SetParent(this.transform);
                sphere.transform.position = newPos;
                stack.Add(sphere);
            }
        }

    }

    public void RemoveFromStack(CanvasSphere sphere)
    {
        var index = SphereIndex(sphere);
        var value = (index - 1) / width;
        Debug.Log(value);
        for (int i = index; i < length; i++)
        {
            Debug.Log(i);
            stack[i].gameObject.SetActive(false);
            stack[i].transform.SetParent(ObjectPooler.Instance.transform);
            stack.RemoveAt(index);
        }

        width--;
    }
}
