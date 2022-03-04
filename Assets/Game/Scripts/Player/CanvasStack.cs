using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class CanvasStack : MonoBehaviour
{
    [SerializeField, ReadOnly] private List<List<CanvasSphere>> stack = new List<List<CanvasSphere>>();
    [SerializeField] private Transform rootPoint;
    //x, y
    [SerializeField] private int width, length;
    [SerializeField] private TextMeshProUGUI stackText;
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

    private void Start()
    {
        FirstLayout();
    }
    //private int index;
    private void OnEnable()
    {
        Observer.StackChanged += SetStackText;
        Observer.MoveStackToPosition += MoveStackToPosition;
    }
    private void OnDisable()
    {
        Observer.StackChanged -= SetStackText;
        Observer.MoveStackToPosition -= MoveStackToPosition;
    }

    private void MoveStackToPosition(Transform newPosition)
    {
        rootPoint.SetPositionAndRotation(newPosition.position, newPosition.rotation);
    }

    private void SetStackText()
    {
        stackText.SetText((width * length).ToString());
    }
    private void FirstLayout()
    {
        for(int i = 0; i < width; i++)
        {
            var x = (rootPoint.transform.position.x - i)*stackGap;
            stack.Add(new List<CanvasSphere>());
            for(int j = 0; j < length; j++)
            {
                var z = (rootPoint.position.z - j) * stackGap;
                var sphere = ObjectPooler.Instance.GetPooledSphere();
                sphere.gameObject.SetActive(true);
                sphere.transform.SetParent(this.transform);
                var newPos = new Vector3(x, sphere.transform.localPosition.y, z);
                sphere.transform.position = newPos;
                stack[i].Add(sphere);
            }
        }

        UpdateRoot();
        SetStackText();
    }

    // Update is called once per frame
    void Update()
    {
        StackMovement();
    }
    private void UpdateOffSet()
    {
        offset = Vector3.back * stackGap;
        //offsetFirst = Vector3.back * firstStackGap;
    }



    private void UpdateRoot()
    {
        var tempPos = this.transform.position;
        tempPos.x = rootPoint.transform.position.x + (width / 2 * (stackGap));
        tempPos.z = rootPoint.transform.position.z;

        this.transform.position = tempPos;
    }

    private void StackMovement()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        UpdateOffSet();
        for (int i = 0; i < width; i++)
        {
            for(int j = length -1; j > 0; j--)
            {
                stack[i][j].transform.position = Vector3.Lerp(stack[i][j].transform.position, stack[i][j - 1].transform.position + offset, 0.45f); 
            }
        }
    }
    private int SphereIndex(CanvasSphere sphere)
    {
        return 1;
    }

    private int GetIndex(int i, int j)
    {
        return i*length + j;
    }

    public void UpdateWidth(int amount)
    {
        width += amount;
        if(amount < 0)
        {
            for(int i= ((width-amount)-1); i >= width; i--)
            {
                for(int j = 0; j < stack[i].Count; j++)
                {
                    stack[i][j].gameObject.SetActive(false);
                    stack[i][j].transform.SetParent(ObjectPooler.Instance.transform);
                }
                stack[i].Clear();
                stack.RemoveAt(i);
            }
        }
        else
        {
            for (int i = width - amount; i < width; i++)
            {
                var x = stack[i - 1][0].transform.position.x - stackGap;
                stack.Add(new List<CanvasSphere>());
                for (int j = 0; j < length; j++)
                {
                    var z = rootPoint.position.z;
                    var sphere = ObjectPooler.Instance.GetPooledSphere();
                    sphere.gameObject.SetActive(true);
                    sphere.transform.SetParent(this.transform);
                    var newPos = new Vector3(x, sphere.transform.localPosition.y, z);
                    sphere.transform.position = newPos;
                    stack[i].Insert(0, sphere);
                }
            }
        }
        Observer.StackChanged?.Invoke();
        UpdateRoot();
    }

    public void UpdateLength(int amount)
    {
        length += amount;
        if(amount < 0)
        {
            for(int i = 0; i < width; i++)
            {
                for(int j = stack[i].Count - 1; j >= length; j--)
                {
                    stack[i][j].gameObject.SetActive(false);
                    stack[i][j].transform.SetParent(ObjectPooler.Instance.transform);
                    stack[i].RemoveAt(j);
                }
            }
        }
        else
        {
            for (int i = 0; i < width; i++)
            {
                var x = (rootPoint.transform.position.x - i) * stackGap;
                for (int j = stack[i].Count; j < length; j++)
                {
                    var z = (rootPoint.position.z - j) * stackGap;
                    var sphere = ObjectPooler.Instance.GetPooledSphere();
                    sphere.gameObject.SetActive(true);
                    sphere.transform.SetParent(this.transform);
                    var newPos = new Vector3(x, sphere.transform.localPosition.y, z);
                    sphere.transform.position = newPos;
                    stack[i].Insert(stack[i].Count, sphere);
                }
            }
        }
        

        Observer.StackChanged?.Invoke();
    }

}
