using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using DG.Tweening;

public class CanvasStack : MonoBehaviour
{
    [SerializeField, ReadOnly] private List<List<CanvasSphere>> stack = new List<List<CanvasSphere>>();
    [SerializeField] private Transform rootPoint;
    //x, y
    [SerializeField] private int width, length;
    [SerializeField] private TextMeshProUGUI stackText;
    private float stackGap => SettingsManager.CanvasSettings.gap;
    private float sideLerpValue => SettingsManager.CanvasSettings.sideLerpValue;
    private float forwardLerpValue => SettingsManager.CanvasSettings.forwardLerpValue;
    //Width gates will remove or
    public int WidthAmountToRemove => stack[stack.Count - 1].Count;
    private int middleIndex => Mathf.FloorToInt((width / 2));
    private int stackCount = 0;
    private Vector3 offset;
    private List<Vector3> oldPositions;
    public int StackCount
    {
        get { return stackCount; }
        set { stackCount = value; }
    }
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
    // Update is called once per frame
    void Update()
    {
        StackMovement();
        FollowRoot();
    }
    public void MoveStackToPosition(Transform newPosition)
    {
        transform.SetParent(rootPoint);
        transform.SetPositionAndRotation(newPosition.position, newPosition.rotation);
    }
    private void SetStackText()
    {
        stackText.SetText((stackCount).ToString());
    }
    private void FirstLayout()
    {
        for(int i = 0; i < width; i++)
        {
            var x = (transform.position.x - i)*stackGap;
            stack.Add(new List<CanvasSphere>());
            for(int j = 0; j < length; j++)
            {
                var z = (transform.position.z - j * stackGap);
                var sphere = ObjectPooler.Instance.GetPooledSphere();
                sphere.Collected();
                sphere.gameObject.SetActive(true);
                sphere.transform.SetParent(this.transform);
                var newPos = new Vector3(x, stackGap/2, z);
                sphere.transform.position = newPos;
                stack[i].Add(sphere);
                stackCount++;
            }
        }
        Observer.StackChanged?.Invoke();
    }
    private void UpdateOffSet()
    {
        offset = Vector3.back * stackGap;
        //offsetFirst = Vector3.back * firstStackGap;
    }
    private void FollowRoot()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        stack[middleIndex][0].transform.position = Vector3.Lerp(stack[middleIndex][0].transform.position, rootPoint.position, sideLerpValue);
        for (int i = 0; i < stack.Count; i++)
        {
            stack[i][0].transform.rotation = Quaternion.Lerp(stack[i][0].transform.localRotation, rootPoint.rotation, sideLerpValue);
            if (i == middleIndex) continue;
            stack[i][0].transform.rotation = rootPoint.rotation;
            var rotationOffset = Mathf.Sin(rootPoint.rotation.y)*(i- middleIndex) *stackGap;
            stack[i][0].transform.position = Vector3.Lerp(stack[i][0].transform.position, stack[middleIndex][0].transform.position + Vector3.left * stackGap*(i- middleIndex), sideLerpValue);
            stack[i][0].transform.position -= rotationOffset*Vector3.back;
            
        }
    }
    private void StackMovement()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        UpdateOffSet();

        for (int i = 0; i < stack.Count; i++)
        {
            for (int j = stack[i].Count-1; j > 0; j--)
            {
                stack[i][j].transform.position = Vector3.Lerp(stack[i][j].transform.position, stack[i][j - 1].transform.position + offset, forwardLerpValue);
            }
        }   
    }
    public void UpdateWidth(int amount)
    {
        
        if(amount < 0)
        {
            width += amount;
            for (int i= ((width-amount)-1); i >= width; i--)
            {
                for(int j = 0; j < stack[i].Count; j++)
                {
                    stack[i][j].Destroyed();
                    stackCount--;
                }
                stack[i].Clear();
                stack.RemoveAt(i);
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                stack.Insert(0 , new List<CanvasSphere>());
                for (int j = 0; j < length; j++)
                {
                    var z = (rootPoint.position.z - j* stackGap);
                    var sphere = ObjectPooler.Instance.GetPooledSphere();
                    sphere.transform.SetParent(this.transform);
                    var newPos = new Vector3(0, sphere.transform.localPosition.y, z);
                    stack[0].Add(sphere);
                    sphere.gameObject.SetActive(true);
                    StartCoroutine(MoveToPosition(sphere, newPos));

                    stackCount++;
                }
                width++;
            }
        }
        Observer.StackChanged?.Invoke();
    }
    public void UpdateLength(int amount)
    {
        
        if(amount < 0)
        {
            length += amount;
            for (int i = 0; i < width; i++)
            {
                for(int j = stack[i].Count - 1; j >= length; j--)
                {
                    stack[i][j].Destroyed();
                    stack[i].RemoveAt(j);
                    
                    stackCount--;
                }
            }
        }
        else
        {
            for (int i = stack.Count -1; i >= 0; i--)
            {
                var x = (rootPoint.transform.position.x - i * stackGap) ;
                for (int j = 0; j < amount; j++)
                {
                    var sphere = ObjectPooler.Instance.GetPooledSphere();
                    sphere.transform.SetParent(this.transform);
                    var newPos = new Vector3(x, sphere.transform.localPosition.y, 0);
                    //sphere.transform.position = newPos;
                    stack[i].Insert(stack[i].Count-1, sphere);
                    sphere.gameObject.SetActive(true);
                    StartCoroutine(MoveToPosition(sphere, newPos));
                    stackCount++;
                }
            }
            length += amount;
        }
        Observer.StackChanged?.Invoke();
    }
    //This is for thin obstacle.
    public void RemoveLine(CanvasSphere sphere)
    {
        int index = 0;
        for(int i = 0; i < stack.Count; i++)
        {
            var contains = stack[i].Contains(sphere);
            if (contains)
            {
                index = i;
            }
        }
        for(int i = stack[index].Count-1; i >= 0 ; i--)
        {
            stack[index][i].Destroyed();
            stackCount--;
        }
        width--;
        stack[index].Clear();
        stack.RemoveAt(index);
        Observer.StackChanged?.Invoke();
    }
    //This is for wall obstacle.
    public void RemoveSpheres(int amount)
    {
        var tempValue = amount;
        int value = 0;
        while(true)
        {
            for (int j = stack[width-1].Count - 1; j > 0; j--)
            {
               
                for (int i = width - 1; i >= 0; i--)
                {
                    stack[i][j].Destroyed();
                    stack[i].RemoveAt(j);
                    stackCount--;
                    tempValue--;
                    if (tempValue <= 0)
                    {
                        Observer.StackChanged?.Invoke();
                        length -= value;
                        return;

                    }                 
                }
                value++;
            }
        }  
    }
    private IEnumerator MoveToPosition(CanvasSphere sphere, Vector3 newPos)
    {
        yield return sphere.transform.DOMove(newPos, 0.2f)
            .SetEase(Ease.Linear)
            .OnComplete(() => OnCompleteMoving(sphere))
            .WaitForCompletion();
    }
    private void OnCompleteMoving(CanvasSphere sphere)
    {
        sphere.Collected();
    }
}
